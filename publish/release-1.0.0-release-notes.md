Holm Lite 1.0.0 — Herm is the lightweight edition of Holm 2.0.1: the notch with music, pop-outs and tools, without friends, Beam or the Holm window.

## Download

| File | For whom |
| --- | --- |
| **Holm-1.0.0-win-x64.zip** | Unzip and run `Holm.exe`. Holm Lite's updater uses this file too. |
| **Holm.exe** | Run the self-contained app directly. No .NET installation needed. |
| **Holm-1.0.0-linux-x64.tar.gz** | Unpack and run `./Holm`. No .NET installation needed. |

Requires Windows 10 (2004 or newer) or Windows 11, x64, or Linux x64 with X11 (Wayland through XWayland).

Holm Lite is unsigned. Windows SmartScreen may warn on first launch, and Smart App Control may block an unsigned app.

Holm Lite runs beside the full Holm: it keeps its own settings and updates only from this repository.

### Added
- **Holm Lite.** The lightweight edition of Holm, from Holm 2.0.1: the notch with music, pop-outs, messages, batteries, calls, the timer, weather, the AI usage rings and the tools (notes, reminders, calculator, clipboard history, mixer, color picker, shelf).
- **Files dropped on the notch** go straight onto the shelf.

### Removed (compared with Holm)
- Friends, invites, chat, GIFs, waves, activity sharing and screen streaming.
- Beam: sending files nearby, with codes or to friends, and sharing with a phone.
- The Holm account, sync and achievements.
- The Holm window with its settings pages. The right-click menu holds the timer, Customize and Quit; everything else keeps its default or is set in `settings.json`.

Holm Lite keeps its settings in its own folder (`%APPDATA%\Holm Lite`, or `~/.config/Holm Lite` on Linux) and updates only from its own releases (ademerci/Holm-Lite), so it runs beside the full Holm without touching it.
