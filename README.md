# Holm Lite

Holm Lite is the lightweight edition of Holm, the Dynamic Island–style notch for Windows and Linux: the notch, music, pop-outs and tools, without friends, Beam and the Holm window. The full Holm lives at https://github.com/ademerci/Holm.

<p align="center">
  <img src="docs/expanded.png" width="480" alt="Expanded notch with media controls and the tool bar">
  <img src="docs/today.png" width="480" alt="Open without music: the time, the weather, the next reminder, the note and quick tools">
</p>

## Features

- **Today at a glance** – idle, the notch shows the time (or the weather) and the next reminder today. Open without music, it is a card for today with a greeting, time and date, the weather, the next reminder, your note, and the tools you reach for most.
- **Tools in the open notch** – notes, reminders, a calculator, the clipboard history, a mixer and a color picker, each a click away in the same top toolbar during idle, playback and tool pages.
- **Notes** – a small note that survives a restart and stays on this PC.
- **Reminders in plain words** – "20 min laundry", "14:30 dentist", "tomorrow 9:00 call" (German works too). The notch rings on time with +10 min; reminders survive a restart.
- **Calculator** – type a sum, the result is there at once; click it to copy.
- **Clipboard history** – the last 20 texts and pictures you copied; click one to put it back. Passwords that password managers mark as private are never read or kept, and a copied password leaves the clipboard after 30 seconds if it's still there.
- **Color picker** – click anywhere on the screen to copy that color's hex code. The arrow keys move the cursor one pixel (ten with Shift) and Enter picks, for the exact pixel.
- **Text from screenshots** – Copy text on the screenshot pop-out reads the text with Windows' own OCR, on your PC.
- **Volume per app** – a slider for each app that plays sound; click its icon to mute it.
- **Weather** – temperature in the idle notch, the sky and the next hour's rain when it's open. Holm uses Windows approximate location.
- **Quiet hours** – from one time to another, messages do not pop out.
- **Sleep timer** – the moon in the open player stops the music after 15, 30 or 60 minutes.
- **Heads-ups** – Caps Lock on or off, a USB stick plugged in (Open, Eject), the internet gone and back, and once when the system drive runs low.
- **Media controls** – cover, title, album, seek bar, play/pause, skip, shuffle and repeat, volume.
- **Equalizer that follows the music** – the four bars in the closed notch move with the sound that plays. 
- **Album colours** – the notch is washed in a blurred copy of the cover and crossfades when the track changes.
- **Volume pop-out** – change the volume with your keys or the taskbar and the notch shows it.
- **Output device** – the button next to the volume lists your speakers, headphones and headsets; pick one and all sound goes there.
- **Bluetooth** – the Bluetooth button next to the volume lists your paired devices, with a tick at the connected ones.
- **VPN** – while a VPN is connected, a small lock sits in the idle notch and a green one next to the batteries.
- **Microphone and camera dot** – small dots inside the closed notch show active recording: orange for the microphone, green for the camera.
- **Calls** – while you're in a call, the notch shows the app and how long the call runs, with a red mark when your microphone is muted.
- **Shelf** – drop files or folders on the notch to park them.
- **Screenshots** – take one and it pops out of the notch, with Shelf; click the picture to open it.
- **Timer** – right-click and pick 1 minute to 1 hour. The time left counts down in the notch.
- **Separate sizes** – choose Small, Normal, Large or Extra large independently for the collapsed notch and the expanded panel.
- **Battery levels** for your laptop and Bluetooth devices.
- **Live activities** – short pop-outs when headphones connect, the charger is plugged in or the battery runs low.
- **AI button** – one click opens an AI app or website.
- **Messages** from WhatsApp, Discord, Outlook, Telegram, Signal and Slack. A new message pops out of the notch with who wrote and the first line, and a badge counts what you haven't seen yet.
- **Downloads** – while your browser saves a file to Downloads, the notch shows its name, size and speed; when it's done, Open or Show it in Explorer with one click.
- **Stays out of your way** – hides during fullscreen games and videos, never steals focus.
- **Right-click menu** – timer, sleep timer, Customize (messages, quiet hours, weather, AI button, collapsed and expanded size, bounce) and Quit. Everything else is read from settings.json in %APPDATA%\Holm Lite on Windows or the matching config folder on Linux, and every feature is on by default.

## Lightweight

Avalonia on .NET 10, with event driven updates where the system provides them.

| State | CPU | RAM (Task Manager) | Working set |
| --- | --- | --- | --- |
| Idle | ~0.02–0.03 % | ~20–35 MB | ~55–80 MB |
| Music playing, equalizer animated | ~0.3–0.5 % | ~45–55 MB | ~95–120 MB |

## Messages

Holm Lite reads the notifications of supported messengers from the Windows notification center. It also reads numeric badges: existing unread counts appear at startup, and an increase pops out the app name and count.

## Requirements

Windows 10 (2004 or newer) or Windows 11, x64; or Linux x64 with X11 (under Wayland Holm Lite runs through XWayland). On Linux, PulseAudio or PipeWire, BlueZ, NetworkManager and UPower are used where present, and `tesseract` for copying text from screenshots.

## Build

You need the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```powershell
# Windows x64, one self-contained file
dotnet publish Holm.App/Holm.App.csproj -c Release -r win-x64 --self-contained -p:PublishSingleFile=true -p:PublishReadyToRun=true -o publish
# Linux x64
dotnet publish Holm.App/Holm.App.csproj -c Release -r linux-x64 --self-contained -p:PublishSingleFile=true -p:PublishReadyToRun=true -o publish
```

This gives you `publish/Holm.exe` (Windows) or `publish/Holm` (Linux).

## License

Open source under the [MIT License](LICENSE).

The AI usage rings and the Claude limit reading are ported from [Codenotch](https://github.com/vinzdg/codenotch) by Vinz (MIT License, © 2026 Vinz).