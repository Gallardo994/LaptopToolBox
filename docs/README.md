# Laptop ToolBox

![main](https://github.com/gallardo994/LaptopToolBox/blob/main/docs/screenshot2.png?raw=true)

## What is this?

An application to control laptop configurations, overclock, undervolt, and etc.

Main goals:
- WinUI3, following Microsoft's styleguides
- Smooth and responsive animated interface
- Zero interface hickups, proper thread scheduling
- Resizable window
- Readable code, with MVVM and following SRP
- Modular functionality, with further ability to port to any laptop brand

Current functionality:

Asus:
- Performance Modes switching + hotkey support (M4)
- Re-open Laptop ToolBox with a hotkey (M5) + reopen existing instance if already running when opening same .exe file
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
- NVIDIA GPU Memory/Core clock control
- SPL/SPPT/FPPT control for AMD CPUs
- CPU/GPU fan control with in-BIOS presets available
- CPU Monitoring
- In-App self-updates with notifications and periodic checking
- Sensors information
- Power draw information
- RAM usage information
- Driver & BIOS updates notifications
- Notification badges

## Tested on

Asus:
- G733PY
- G513RW

## TODO
- Detect CPU models and undervolt support
- Implement undervolt for Intel CPUs
- Implement hints for UI elements
- Implement tutorials for UI elements
- Implement hardware information
- Implement GPU load information
- Implement ability to close apps running on discrete GPU
- Modify UI for styleguide compliance
- Maybe more?

The code is currently "in progress", meaning that most parts still require rework and lots of features are non-functional.

## How can you help?
- Improve UI
- Implement new features
- Localize
- Test on your device and report the results
- Anything you want to improve the project!

## Disclaimer

USE AT YOUR OWN RISK. I AM NOT RESPONSIBLE FOR ANYTHING THAT MIGHT HAPPEN 
TO YOUR DEVICE IN ANY MANNER.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

Disclaimers "ROG", "TUF", and "Armoury Crate" are trademarked by and belong to AsusTek Computer, Inc. I make no claims to these or any assets belonging to AsusTek Computer and use them purely for informational purposes only.