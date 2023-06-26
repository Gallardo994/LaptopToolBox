# G-Helper Fork

![main](https://github.com/gallardo994/g-helper/blob/main/docs/screenshot1.png?raw=true)

## What is this?

A complete rewrite of original G-Helper with main goals:
- WinUI3, following Microsoft's styleguides
- Smooth and responsive animated interface
- Zero interface hickups, proper thread scheduling
- Resizable window
- Readable code, with MVVM and following SRP
- Modular functionality, with further ability to port to any other laptop brand

Current functionality:
- Performance Modes switching + hotkey support (M4)
- Re-open GHelper with a hotkey (M5) + reopen existing instance if already running when opening same .exe file
- Always Awake mode (FN+C to toggle)
- Night Light switch (FN+V to toggle)
- Auto Overdrive mode (switches display HZ based on battery state)
- Battery limit + temporary unlimiter until charged completely or removed the power source
- CPU/GPU information on home page
- Autostart + start minimized
- Vendor services start/stop
- Keyboard backlight hotkeys (FN+F2, FN+F3)
- Display backlight hotkeys (FN+F7, FN+F8)
- Driver and BIOS updates
- Aura control (without FN+F4 key yet, also not designed properly at this moment)
- AMD CPUs undervolt
- TouchPad Enable/Disable (FN+F10, with notifications)
- CPU/GPU fans RPM information
- Microphone switch (M3, with notifications)

## TODO
- Detect CPU models and undervolt support
- Implement undervolt for Intel CPUs
- Implement Driver & BIOS updates notifications
- Implement custom profiles
- Implement power control
- Implement fans control
- Implement hints for UI elements
- Implement tutorials for UI elements
- Implement NVIDIA GPU control
- Implement sensors information
- Implement hardware information
- Implement CPU/GPU/RAM load information
- Implement ability to close apps running on discrete GPU
- Modify UI for styleguide compliance
- Maybe more?

The code is currently "in progress", meaning that most parts still require rework and lots of features are non-functional.

## How can I help?
- Improve UI
- Implement new features
- Localize
- Anything you want to improve the project!

## Why?

The original upstream refuses to accept any PRs without giving any explanations.
This fork aims to improve the original project both in terms of code and features.

Any PRs and improvements are welcome.