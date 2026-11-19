using System;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Input.Platform;
using Avalonia.Media;
using Avalonia.Threading;
using WinNotch;

namespace Holm.App;

// What the notch's task view (progress, done, failed), memory hand-back and the bounce menu need, kept from the
// Beam and settings code that Holm Lite leaves out.
public sealed partial class NotchWindow
{
    static readonly (string Name, double Level)[] BounceLevels = { ("Subtle", 0.5), ("Normal", 1), ("Lively", Spring.MaxBounce) };
    bool _busy;
    CancellationTokenSource? _taskCts;
    long _acceptTick;
    DateTime _lastProgressUi;

    CancellationTokenSource BeginTask()
    {
        _taskHide.Stop();
        _taskHide.Interval = TimeSpan.FromSeconds(5);
        _taskCts?.Dispose();
        _taskCts = new CancellationTokenSource();
        _busy = true;
        return _taskCts;
    }

    // Ignore the second click of a double-click on Accept, which lands on the Cancel that replaces it.
    void CancelTask() { if (Environment.TickCount64 - _acceptTick > 600) _taskCts?.Cancel(); }

    /// <summary>Progress, at most a few times a second (every 64 KB would be far too many redraws).</summary>
    void ShowProgress(string title, string subtitle, double fraction, bool sending)
    {
        if (!_busy) return;
        var now = DateTime.UtcNow;
        if (fraction > 0 && fraction < 1 && now - _lastProgressUi < TimeSpan.FromMilliseconds(150)) return;
        _lastProgressUi = now;
        ShowTask(sending ? "" : "", ShareBlue, title, $"{subtitle} · {Math.Round(fraction * 100)}%", fraction, null, ("Cancel", CancelTask));
    }

    void EndTask(string icon, IBrush circle, string title, string subtitle,
                 (string Label, Action Act)? primary = null, (string Label, Action Act)? secondary = null)
    {
        _busy = false;
        ShowTask(icon, circle, title, subtitle, null, primary, secondary ?? ("OK", HideTask));
        _taskHide.Interval = TimeSpan.FromSeconds(5);
        _taskHide.Start();
    }

    void Fail(Exception ex, CancellationTokenSource cts, string title)
    {
        if (ex is OperationCanceledException && cts.IsCancellationRequested)
        {
            EndTask("", RedBrush, "Cancelled", "");
            return;
        }
        Diag.Log($"{title}: {ex.GetType().Name}: {ex.Message}");
        string reason = ex switch
        {
            SocketException => "Couldn't reach them",

            EndOfStreamException => "The connection was closed",
            IOException => "The connection dropped – try again.",
            _ => "Something went wrong",
        };
        EndTask("", RedBrush, title, reason);
    }

    async Task<bool> TrySetClipboardAsync(string text)
    {
        if (Clipboard is not { } clipboard) return false;
        for (int attempt = 0; attempt < 3; attempt++)
        {
            try { await clipboard.SetTextAsync(text); return true; }
            catch (Exception ex) when (ex is not OutOfMemoryException) { await Task.Delay(30); } // another app has it open
        }
        return false;
    }

    DispatcherTimer? _trimTimerField;
    DispatcherTimer _trimTimer => _trimTimerField ??= OneShot(6000, () => { if (!_hovered) _platform.TrimMemory(); });
    static DispatcherTimer OneShot(double ms, Action act)
    {
        var timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(ms) };
        timer.Tick += (_, _) => { timer.Stop(); act(); };
        return timer;
    }

    void ApplyBounce()
    {
        Spring.Bounce = _settings.Bounce ? _settings.BounceLevel : 0;
        Spring.Jelly = _settings.Jelly;
        if (!Spring.Jelly)
        {
            _dragLean.Snap(0);
            _dragSkew.AngleX = 0;
        }
    }

    /// <summary>Customize › Bounce: Subtle, Normal or Lively, while Bouncy motion is on.</summary>
    void FillBounceMenu(MenuItem menu)
    {
        menu.Items.Clear();
        menu.IsEnabled = _settings.Bounce;
        foreach (var (name, level) in BounceLevels)
        {
            var item = new MenuItem { Header = name, ToggleType = MenuItemToggleType.Radio, IsChecked = Math.Abs(_settings.BounceLevel - level) < 0.01 };
            item.Click += (_, _) =>
            {
                _settings.BounceLevel = level;
                _settings.Save();
                ApplyBounce();
            };
            menu.Items.Add(item);
        }
    }
}
