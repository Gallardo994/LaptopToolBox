using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using GHelper.AsusAcpi;
using GHelper.Core;
using GHelper.Modules;
using GHelper.Powerline;
using GHelper.Settings;
using GHelper.Tray;
using Ninject;
using Serilog;
using static NativeMethods;

namespace GHelper
{

    static class Program
    {
        public static AsusACPI? acpi;

        public static ISettingsFormController _settingsFormController; // TODO: Inject only
        public static SettingsForm _settingsForm; // TODO: Inject only

        public static IntPtr unRegPowerNotify;

        private static long lastAuto;
        private static long lastTheme;

        public static IInputDispatcher _inputDispatcher; // TODO: Inject only

        private static IPowerlineStatusProvider _powerlineStatusProvider; // TODO: Inject only
        
        public static ITrayProvider _trayProvider; // TODO: Inject only

        // The main entry point for the application
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
                    .WriteTo.Console()
                    .MinimumLevel.Debug()
                    .CreateLogger();
                
                Log.Debug("Trying to start");
            
                var kernel = new StandardKernel();
                kernel.Load(Assembly.GetExecutingAssembly());
                
                Log.Debug("Kernel loaded");
                
                
                _trayProvider = kernel.Get<ITrayProvider>();
                _settingsForm = kernel.Get<SettingsForm>();
                _settingsFormController = kernel.Get<ISettingsFormController>();
                _powerlineStatusProvider = kernel.Get<IPowerlineStatusProvider>();

                var core = kernel.Get<ICoreRunner>();
                core.Run(args);

                var acpiChecker = kernel.Get<IAsusAcpiErrorProvider>();

                if (!acpiChecker.InvokeCheckAndNotify())
                {
                    return;
                }
                
                var acpiProvider = kernel.Get<IAsusAcpiProvider>();
                acpiProvider.TryGet(out acpi);

                Log.Debug("------------");
                Log.Debug("App launched: " + AppConfig.GetModel() + " :" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + CultureInfo.CurrentUICulture + (ProcessHelper.IsUserAdministrator() ? "." : ""));

                Application.EnableVisualStyles();

                HardwareControl.RecreateGpuControl();

                var ds = _settingsForm.Handle;
                
                _inputDispatcher = kernel.Get<IInputDispatcher>();

                _settingsForm.InitAura();
                _settingsForm.InitMatrix();
                _settingsForm.SetStartupCheck(Startup.IsScheduled());


                SetAutoModes();

                // Subscribing for system power change events
                _powerlineStatusProvider.PowerlineStatusChanged += SystemEvents_PowerModeChanged;
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;

                // Subscribing for monitor power on events
                PowerSettingGuid settingGuid = new NativeMethods.PowerSettingGuid();
                unRegPowerNotify = NativeMethods.RegisterPowerSettingNotification(ds, settingGuid.ConsoleDisplayState, NativeMethods.DEVICE_NOTIFY_WINDOW_HANDLE);


                string action = "";
                if (args.Length > 0)
                {
                    action = args[0];
                }
                
                if (Environment.CurrentDirectory.Trim('\\') == Application.StartupPath.Trim('\\') || action.Length > 0)
                {
                    _settingsFormController.Toggle(action);
                }

                Application.Run();
            } catch (Exception e)
            {
                Log.Error(e, "Unhandled exception in Main");
                throw;
            }
        }



        static void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
        {

            if (Math.Abs(DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastTheme) < 2000) return;

            switch (e.Category)
            {
                case UserPreferenceCategory.General:
                    bool changed = _settingsForm.InitTheme();
                    if (changed)
                    {
                        Debug.WriteLine("Theme Changed");
                        lastTheme = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    }

                    if (_settingsForm.fans is not null && _settingsForm.fans.Text != "")
                        _settingsForm.fans.InitTheme();

                    if (_settingsForm.keyb is not null && _settingsForm.keyb.Text != "")
                        _settingsForm.keyb.InitTheme();

                    if (_settingsForm.updates is not null && _settingsForm.updates.Text != "")
                        _settingsForm.updates.InitTheme();

                    break;
            }
        }



        public static void SetAutoModes(bool powerChanged = false)
        {

            if (Math.Abs(DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastAuto) < 3000) return;
            lastAuto = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            Log.Debug("AutoSetting for " + _powerlineStatusProvider.IsPlugged);

            _inputDispatcher.Init();

            _settingsForm.SetBatteryChargeLimit(AppConfig.Get("charge_limit"));
            _settingsForm.AutoPerformance(powerChanged);

            bool switched = _settingsForm.AutoGPUMode();

            if (!switched)
            {
                _settingsForm.InitGPUMode();
                _settingsForm.AutoScreen();
            }

            _settingsForm.AutoKeyboard();
            _settingsForm.matrix.SetMatrix();
        }

        private static void SystemEvents_PowerModeChanged(PowerLineStatus status)
        {
            Log.Debug("Power Mode Changed");
            SetAutoModes(true);
        }

        static void OnExit(object sender, EventArgs e)
        {
            _trayProvider.SetVisible(false);
            NativeMethods.UnregisterPowerSettingNotification(unRegPowerNotify);
            Application.Exit();
        }
    }

}