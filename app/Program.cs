using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using GHelper.Modules;
using GHelper.Tray;
using Ninject;
using Serilog;
using static NativeMethods;

namespace GHelper
{

    static class Program
    {
        public static AsusACPI? acpi;

        public static SettingsForm settingsForm;

        public static IntPtr unRegPowerNotify;

        private static long lastAuto;
        private static long lastTheme;

        public static IInputDispatcher _inputDispatcher;

        private static PowerLineStatus isPlugged = SystemInformation.PowerStatus.PowerLineStatus;
        
        public static ITrayProvider _trayProvider;

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
                settingsForm = new SettingsForm();
                
                string action = "";
                if (args.Length > 0) action = args[0];

                string language = AppConfig.GetString("language"); // TODO: Move to IAppConfig

                if (language != null && language.Length > 0)
                    Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
                else
                {
                    var culture = CultureInfo.CurrentUICulture;
                    if (culture.ToString() == "kr") culture = CultureInfo.GetCultureInfo("ko");
                    Thread.CurrentThread.CurrentUICulture = culture;
                }

                Debug.WriteLine(CultureInfo.CurrentUICulture);

                ProcessHelper.CheckAlreadyRunning();

                try
                {
                    acpi = new AsusACPI();
                }
                catch
                {
                    DialogResult dialogResult = MessageBox.Show(Properties.Strings.ACPIError, Properties.Strings.StartupError, MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo("https://www.asus.com/support/FAQ/1047338/") { UseShellExecute = true });
                    }

                    Application.Exit();
                    return;
                }

                Log.Debug("------------");
                Log.Debug("App launched: " + AppConfig.GetModel() + " :" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + CultureInfo.CurrentUICulture + (ProcessHelper.IsUserAdministrator() ? "." : ""));

                Application.EnableVisualStyles();

                HardwareControl.RecreateGpuControl();

                var ds = settingsForm.Handle;
                
                _inputDispatcher = kernel.Get<IInputDispatcher>();

                settingsForm.InitAura();
                settingsForm.InitMatrix();
                settingsForm.SetStartupCheck(Startup.IsScheduled());


                SetAutoModes();

                // Subscribing for system power change events
                SystemEvents.PowerModeChanged += SystemEvents_PowerModeChanged;
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;

                // Subscribing for monitor power on events
                PowerSettingGuid settingGuid = new NativeMethods.PowerSettingGuid();
                unRegPowerNotify = NativeMethods.RegisterPowerSettingNotification(ds, settingGuid.ConsoleDisplayState, NativeMethods.DEVICE_NOTIFY_WINDOW_HANDLE);


                if (Environment.CurrentDirectory.Trim('\\') == Application.StartupPath.Trim('\\') || action.Length > 0)
                {
                    SettingsToggle(action);
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
                    bool changed = settingsForm.InitTheme();
                    if (changed)
                    {
                        Debug.WriteLine("Theme Changed");
                        lastTheme = DateTimeOffset.Now.ToUnixTimeMilliseconds();
                    }

                    if (settingsForm.fans is not null && settingsForm.fans.Text != "")
                        settingsForm.fans.InitTheme();

                    if (settingsForm.keyb is not null && settingsForm.keyb.Text != "")
                        settingsForm.keyb.InitTheme();

                    if (settingsForm.updates is not null && settingsForm.updates.Text != "")
                        settingsForm.updates.InitTheme();

                    break;
            }
        }



        public static void SetAutoModes(bool powerChanged = false)
        {

            if (Math.Abs(DateTimeOffset.Now.ToUnixTimeMilliseconds() - lastAuto) < 3000) return;
            lastAuto = DateTimeOffset.Now.ToUnixTimeMilliseconds();

            isPlugged = SystemInformation.PowerStatus.PowerLineStatus;
            Log.Debug("AutoSetting for " + isPlugged.ToString());

            _inputDispatcher.Init();

            settingsForm.SetBatteryChargeLimit(AppConfig.Get("charge_limit"));
            settingsForm.AutoPerformance(powerChanged);

            bool switched = settingsForm.AutoGPUMode();

            if (!switched)
            {
                settingsForm.InitGPUMode();
                settingsForm.AutoScreen();
            }

            settingsForm.AutoKeyboard();
            settingsForm.matrix.SetMatrix();
        }

        private static void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            if (SystemInformation.PowerStatus.PowerLineStatus == isPlugged) return;
            Log.Debug("Power Mode Changed");
            SetAutoModes(true);
        }



        public static void SettingsToggle(string action = "")
        {
            if (settingsForm.Visible) settingsForm.HideAll();
            else
            {
                settingsForm.Show();
                settingsForm.Activate();
                settingsForm.VisualiseGPUMode();

                switch (action)
                {
                    case "gpu":
                        Startup.ReScheduleAdmin();
                        settingsForm.FansToggle();
                        break;
                    case "gpurestart":
                        settingsForm.RestartGPU(false);
                        break;
                    case "services":
                        settingsForm.keyb = new Extra();
                        settingsForm.keyb.Show();
                        settingsForm.keyb.ServiesToggle();
                        break;
                }
            }
        }

        static void TrayIcon_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                SettingsToggle();
            }

        }



        static void OnExit(object sender, EventArgs e)
        {
            _trayProvider.SetVisible(false);
            NativeMethods.UnregisterPowerSettingNotification(unRegPowerNotify);
            Application.Exit();
        }
    }

}