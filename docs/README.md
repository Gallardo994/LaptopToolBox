# G-Helper Fork

## What is this?

Same G-Helper but:
- Always-on-top by default
- Refresh support in Drivers & BIOS update
- Animated toasts
- Windows 10+ support only
- Notifications support (currently unused)
- Fixed UI lag on startup mode change
- More localizations
- Improved codebase

Planned:
- Overall overhaul of the code
- Fix all UI stutters due to executing long-running tasks on the main thread
- Implement undervolt for AMD CPUs
- Improve overall UI
- Driver & Bios update notifications
- New version notifications
- "Max charge" button (MacOS-style) to ignore battery charge limit only once
- Fix display detection if Mux is switched to dGPU-only
- Optimize overall code and reduce CPU usage, fix tons of allocations

Regressions:
- TODO: Right-clicking tray icon currently has no effect

## Why?

The original upstream refuses to accept any PRs without giving any explanations.
This fork aims to improve the original project both in terms of code and features.

Any PRs and improvements are welcome.