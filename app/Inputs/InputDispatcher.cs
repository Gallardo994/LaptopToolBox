using System.Diagnostics;
using System.Management;
using GHelper.Modules;
using HidLibrary;
using Microsoft.Win32;
using NAudio.CoreAudioApi;
using Ninject;
using Serilog;

namespace GHelper.Inputs
{
    public class InputDispatcher : IInputDispatcher
    {
        private IKeyboardListener _keyboardListener;
        
        System.Timers.Timer timer = new System.Timers.Timer(1000);
        public bool backlightActivity = true;

        public static Keys keyProfile = Keys.F5;
        public static Keys keyApp = Keys.F12;

        KeyboardHook hook = new KeyboardHook();

        [Inject]
        public InputDispatcher()
        {
            byte[] result = Program.acpi.DeviceInit();
            Debug.WriteLine($"Init: {BitConverter.ToString(result)}");

            Program.acpi.SubscribeToEvents(WatcherEventArrived);
            //Task.Run(Program.acpi.RunListener);

            hook.KeyPressed += new EventHandler<KeyPressedEventArgs>(KeyPressed);

            RegisterKeys();

            timer.Elapsed += Timer_Elapsed;

        }

        private void Timer_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (GetBacklight() == 0) return;

            TimeSpan iddle = NativeMethods.GetIdleTime();
            int kb_timeout;

            if (SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online)
                kb_timeout = AppConfig.Get("keyboard_ac_timeout", 0);
            else
                kb_timeout = AppConfig.Get("keyboard_timeout", 60);

            if (kb_timeout == 0) return;

            if (backlightActivity && iddle.TotalSeconds > kb_timeout)
            {
                backlightActivity = false;
                AsusUSB.ApplyBrightness(0, "Timeout");
            }

            if (!backlightActivity && iddle.TotalSeconds < kb_timeout)
            {
                backlightActivity = true;
                SetBacklightAuto();
            }

            //Debug.WriteLine(iddle.TotalSeconds);
        }

        public void Init()
        {
            if (_keyboardListener is not null)
            {
                _keyboardListener.Dispose();
            }

            Program.acpi.DeviceInit();

            if (!OptimizationService.IsRunning())
                _keyboardListener = new KeyboardListener(HandleEvent);
            else
                Log.Debug("Optimization service is running");

            InitBacklightTimer();
        }

        public void InitBacklightTimer()
        {
            timer.Enabled = (AppConfig.Get("keyboard_timeout") > 0 && SystemInformation.PowerStatus.PowerLineStatus != PowerLineStatus.Online) ||
                            (AppConfig.Get("keyboard_ac_timeout") > 0 && SystemInformation.PowerStatus.PowerLineStatus == PowerLineStatus.Online);
        }



        public void RegisterKeys()
        {
            hook.UnregisterAll();

            // CTRL + SHIFT + F5 to cycle profiles
            if (AppConfig.Get("keybind_profile") != -1) keyProfile = (Keys)AppConfig.Get("keybind_profile");
            if (AppConfig.Get("keybind_app") != -1) keyApp = (Keys)AppConfig.Get("keybind_app");

            string actionM1 = AppConfig.GetString("m1"); // TODO: Move to IAppConfig
            string actionM2 = AppConfig.GetString("m2"); // TODO: Move to IAppConfig

            if (keyProfile != Keys.None) hook.RegisterHotKey(ModifierKeys.Shift | ModifierKeys.Control, keyProfile);
            if (keyApp != Keys.None) hook.RegisterHotKey(ModifierKeys.Shift | ModifierKeys.Control, keyApp);

            if (!AppConfig.ContainsModel("Z13"))
                if (actionM1 is not null && actionM1.Length > 0) hook.RegisterHotKey(ModifierKeys.None, Keys.VolumeDown);
                if (actionM2 is not null && actionM2.Length > 0) hook.RegisterHotKey(ModifierKeys.None, Keys.VolumeUp);

            // FN-Lock group

            if (AppConfig.Is("fn_lock") && !AppConfig.ContainsModel("VivoBook"))
                for (Keys i = Keys.F1; i <= Keys.F11; i++) hook.RegisterHotKey(ModifierKeys.None, i);

        }

        static void CustomKey(string configKey = "m3")
        {
            string command = AppConfig.GetString(configKey + "_custom");
            int intKey;

            try
            {
                intKey = Convert.ToInt32(command, 16);
            }
            catch
            {
                intKey = -1;
            }


            if (intKey > 0)
                KeyboardHook.KeyPress((Keys)intKey);
            else
                LaunchProcess(command);

        }

        public void KeyPressed(object sender, KeyPressedEventArgs e)
        {

            if (e.Modifier == ModifierKeys.None)
            {
                Log.Debug(e.Key.ToString());

                if (AppConfig.ContainsModel("Z13"))
                {
                    switch (e.Key)
                    {
                        case Keys.F2:
                            KeyboardHook.KeyPress(Keys.VolumeDown);
                            return;
                        case Keys.F3:
                            KeyboardHook.KeyPress(Keys.VolumeUp);
                            return;
                        case Keys.F4:
                            KeyProcess("m3");
                            return;
                        case Keys.F11:
                            HandleEvent(199);
                            return;
                    }
                }

                if (AppConfig.ContainsModel("GA401I") && !AppConfig.ContainsModel("GA401IHR"))
                {
                    switch (e.Key)
                    {
                        case Keys.F2:
                            KeyboardHook.KeyPress(Keys.MediaPreviousTrack);
                            return;
                        case Keys.F3:
                            KeyboardHook.KeyPress(Keys.MediaPlayPause);
                            return;
                        case Keys.F4:
                            KeyboardHook.KeyPress(Keys.MediaNextTrack);
                            return;
                    }
                }


                switch (e.Key)
                {
                    case Keys.F1:
                        KeyboardHook.KeyPress(Keys.VolumeMute);
                        break;
                    case Keys.F2:
                        HandleEvent(197);
                        break;
                    case Keys.F3:
                        HandleEvent(196);
                        break;
                    case Keys.F4:
                        KeyProcess("fnf4");
                        break;
                    case Keys.F5:
                        KeyProcess("fnf5");
                        break;
                    case Keys.F6:
                        KeyboardHook.KeyPress(Keys.Snapshot);
                        break;
                    case Keys.F7:
                        if (AppConfig.ContainsModel("TUF"))
                            Program._settingsForm.BeginInvoke(Program._settingsForm.RunToast, ScreenBrightness.Adjust(-10) + "%", ToastIcon.BrightnessDown);
                        HandleEvent(16);
                        break;
                    case Keys.F8:
                        if (AppConfig.ContainsModel("TUF")) 
                            Program._settingsForm.BeginInvoke(Program._settingsForm.RunToast, ScreenBrightness.Adjust(+10) + "%", ToastIcon.BrightnessUp);
                        HandleEvent(32);
                        break;
                    case Keys.F9:
                        KeyboardHook.KeyWinPress(Keys.P);
                        break;
                    case Keys.F10:
                        HandleEvent(107);
                        break;
                    case Keys.F11:
                        HandleEvent(108);
                        break;
                    case Keys.F12:
                        KeyboardHook.KeyWinPress(Keys.A);
                        break;
                    case Keys.VolumeDown:
                        KeyProcess("m1");
                        break;
                    case Keys.VolumeUp:
                        KeyProcess("m2");
                        break;
                    default:
                        break;
                }
            }

            if (e.Modifier == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                if (e.Key == keyProfile) Program._settingsForm.CyclePerformanceMode();
                if (e.Key == keyApp) Program._settingsOpenFormRequest.Invoke();
            }


        }


        public static void KeyProcess(string name = "m3")
        {
            string action = AppConfig.GetString(name);

            if (action is null || action.Length <= 1)
            {
                if (name == "m4")
                    action = "ghelper";
                if (name == "fnf4")
                    action = "aura";
                if (name == "fnf5")
                    action = "performance";
                if (name == "m3" && !OptimizationService.IsRunning())
                    action = "micmute";
                if (name == "fnc")
                    action = "fnlock";
            }

            switch (action)
            {
                case "mute":
                    KeyboardHook.KeyPress(Keys.VolumeMute);
                    break;
                case "play":
                    KeyboardHook.KeyPress(Keys.MediaPlayPause);
                    break;
                case "screenshot":
                    KeyboardHook.KeyPress(Keys.Snapshot);
                    break;
                case "screen":
                    Log.Debug("Screen off toggle");
                    NativeMethods.TurnOffScreen(Program._settingsForm.Handle);
                    break;
                case "miniled":
                    Program._settingsForm.BeginInvoke(Program._settingsForm.ToogleMiniled);
                    break;
                case "aura":
                    Program._settingsForm.BeginInvoke(Program._settingsForm.CycleAuraMode);
                    break;
                case "performance":
                    Program._settingsForm.BeginInvoke(Program._settingsForm.CyclePerformanceMode);
                    break;
                case "ghelper":
                    Program._settingsForm.BeginInvoke(delegate
                    {
                        Program._settingsOpenFormRequest.Invoke();
                    });
                    break;
                case "fnlock":
                    ToggleFnLock();
                    break;
                case "micmute":
                    using (var enumerator = new MMDeviceEnumerator())
                    {
                        var commDevice = enumerator.GetDefaultAudioEndpoint(DataFlow.Capture, Role.Communications);
                        bool muteStatus = !commDevice.AudioEndpointVolume.Mute;
                        commDevice.AudioEndpointVolume.Mute = muteStatus;
                        Program._settingsForm.BeginInvoke(Program._settingsForm.RunToast, muteStatus ? "Muted" : "Unmuted", muteStatus ? ToastIcon.MicrophoneMute : ToastIcon.Microphone);
                    }
                    break;
                case "brightness_up":
                    HandleEvent(32);
                    break;
                case "brightness_down":
                    HandleEvent(16);
                    break;
                case "custom":
                    CustomKey(name);
                    break;

                default:
                    break;
            }
        }

        static bool GetTouchpadState()
        {
            using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\PrecisionTouchPad\Status", false))
            {
                return (key?.GetValue("Enabled")?.ToString() == "1");
            }
        }

        static void ToggleFnLock()
        {
            int fnLock = AppConfig.Is("fn_lock") ? 0 : 1;
            AppConfig.Set("fn_lock", fnLock);

            if (AppConfig.ContainsModel("VivoBook"))
                Program.acpi.DeviceSet(AsusACPI.FnLock, (fnLock == 1) ? 0 : 1, "FnLock");
            else
                Program._settingsForm.BeginInvoke(Program._inputDispatcher.RegisterKeys);

            Program._settingsForm.BeginInvoke(Program._settingsForm.RunToast, "Fn-Lock "+(fnLock==1?"On":"Off"), ToastIcon.FnLock);
        }

        public static void TabletMode()
        {
            bool touchpadState = GetTouchpadState();
            bool tabletState = Program.acpi.DeviceGet(AsusACPI.TabletState) > 0;

            Log.Debug("Tablet: " + tabletState + " Touchpad: " + touchpadState);

            if ((tabletState && touchpadState) || (!tabletState && !touchpadState)) AsusUSB.TouchpadToggle();

        }

        static void HandleEvent(int EventID)
        {
            switch (EventID)
            {
                case 124:    // M3
                    KeyProcess("m3");
                    return;
                case 56:    // M4 / Rog button
                    KeyProcess("m4");
                    return;
                case 174:   // FN+F5
                    Program._settingsForm.BeginInvoke(Program._settingsForm.CyclePerformanceMode);
                    return;
                case 179:   // FN+F4
                case 178:   // FN+F4
                    KeyProcess("fnf4");
                    return;
                case 158:   // Fn + C
                    KeyProcess("fnc");
                    return;
                case 78:    // Fn + ESC
                    ToggleFnLock();
                    return;
                case 189: // Tablet mode 
                    TabletMode();
                    return;
                case 197: // FN+F2
                    SetBacklight(-1);
                    return;
                case 196: // FN+F3
                    SetBacklight(1);
                    return;
                case 199: // ON Z13 - FN+F11 - cycles backlight
                    SetBacklight(4);
                    return;
                case 53:    // FN+F6 on GA-502DU model
                    NativeMethods.TurnOffScreen(Program._settingsForm.Handle);
                    return;
            }

            if (OptimizationService.IsRunning()) return;

            // Asus Optimization service Events 

            switch (EventID)
            {
                case 16: // FN+F7
                    Program.acpi.DeviceSet(AsusACPI.UniversalControl, AsusACPI.Brightness_Down, "Brightness");
                    break;
                case 32: // FN+F8
                    Program.acpi.DeviceSet(AsusACPI.UniversalControl, AsusACPI.Brightness_Up, "Brightness");
                    break;
                case 107: // FN+F10
                    bool touchpadState = GetTouchpadState();
                    AsusUSB.TouchpadToggle();
                    Program._settingsForm.BeginInvoke(Program._settingsForm.RunToast, touchpadState ? "Off" : "On", ToastIcon.Touchpad);
                    break;
                case 108: // FN+F11
                    Program.acpi.DeviceSet(AsusACPI.UniversalControl, AsusACPI.KB_Sleep, "Sleep");
                    break;
                case 106: // Zephyrus DUO special key for turning on/off second display.
                    //Program.acpi.DeviceSet(AsusACPI.UniversalControl, AsusACPI.KB_DUO_SecondDisplay, "SecondDisplay");
                    break;
                case 75: // Zephyrus DUO special key  for changing between arrows and pgup/pgdn
                    //Program.acpi.DeviceSet(AsusACPI.UniversalControl, AsusACPI.KB_DUO_PgUpDn, "PgUpDown");
                    break;
            }

        }


        public static int GetBacklight()
        {
            int backlight_power = AppConfig.Get("keyboard_brightness", 1);
            int backlight_battery = AppConfig.Get("keyboard_brightness_ac", 1);
            bool onBattery = SystemInformation.PowerStatus.PowerLineStatus != PowerLineStatus.Online;

            int backlight;

            //backlight = onBattery ? Math.Min(backlight_battery, backlight_power) : Math.Max(backlight_battery, backlight_power);
            backlight = onBattery ? backlight_battery : backlight_power;

            return Math.Max(Math.Min(3, backlight), 0);
        }

        public static void SetBacklightAuto(bool init = false)
        {
            if (init) AsusUSB.Init();

            //if (!OptimizationService.IsRunning()) 
            AsusUSB.ApplyBrightness(GetBacklight(), "Auto");
        }

        public static void SetBacklight(int delta)
        {
            int backlight_power = AppConfig.Get("keyboard_brightness", 1);
            int backlight_battery = AppConfig.Get("keyboard_brightness_ac", 1);
            bool onBattery = SystemInformation.PowerStatus.PowerLineStatus != PowerLineStatus.Online;

            int backlight = onBattery ? backlight_battery : backlight_power;

            if (delta >= 4)
                backlight = (++backlight % 4);
            else 
                backlight = Math.Max(Math.Min(3, backlight + delta), 0);

            if (onBattery)
                AppConfig.Set("keyboard_brightness_ac", backlight);
            else
                AppConfig.Set("keyboard_brightness", backlight);

            if (!OptimizationService.IsRunning())
            {
                AsusUSB.ApplyBrightness(backlight, "HotKey");
                string[] backlightNames = new string[] { "Off", "Low", "Mid", "Max" };
                Program._settingsForm.BeginInvoke(Program._settingsForm.RunToast, backlightNames[backlight], delta > 0 ? ToastIcon.BacklightUp : ToastIcon.BacklightDown);
            }

        }

        static void LaunchProcess(string command = "")
        {

            try
            {
                string executable = command.Split(' ')[0];
                string arguments = command.Substring(executable.Length).Trim();
                Process proc = Process.Start(executable, arguments);
            }
            catch
            {
                Log.Debug("Failed to run  " + command);
            }


        }



        static void WatcherEventArrived(object sender, EventArrivedEventArgs e)
        {
            if (e.NewEvent is null) return;
            int EventID = int.Parse(e.NewEvent["EventID"].ToString());
            Log.Debug("WMI event " + EventID);
            HandleEvent(EventID);
        }
    }
}
