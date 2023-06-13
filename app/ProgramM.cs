using System;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Windows.Forms;
using GHelper.AsusAcpi;
using GHelper.Core;
using GHelper.Modules;
using GHelper.Powerline;
using GHelper.PowerNotification;
using GHelper.Settings;
using GHelper.Tray;
using GHelper.Updates;
using GHelper.Updates.UI;
using Ninject;
using Serilog;
using static NativeMethods;

namespace GHelper
{

    static class ProgramM
    {
        public static AsusACPI? acpi;

        public static ISettingsFormController _settingsFormController; // TODO: Inject only
        public static SettingsForm _settingsForm; // TODO: Inject only

        public static IPowerNotifier _powerNotifier; // TODO: Inject only

        private static long lastAuto;
        private static long lastTheme;

        public static IInputDispatcher _inputDispatcher; // TODO: Inject only

        private static IPowerlineStatusProvider _powerlineStatusProvider; // TODO: Inject only
        
        public static ITrayProvider _trayProvider; // TODO: Inject only

        // The main entry point for the application
        [STAThread]
        public static void MainA(StandardKernel kernel, string[] args)
        {
            try
            {
                _trayProvider = kernel.Get<ITrayProvider>();
                _settingsForm = kernel.Get<SettingsForm>();
                _settingsFormController = kernel.Get<ISettingsFormController>();
                _powerlineStatusProvider = kernel.Get<IPowerlineStatusProvider>();

                var core = kernel.Get<ICoreRunner>();
                if (!core.Run(args))
                {
                    return;
                }
                
                var versionChecker = kernel.Get<IUpdatesScheduler>();
                versionChecker.CheckNow();
                versionChecker.ReSchedule(new TimeSpan(0, 0, 0, 30, 0));

                var updatesWindowController = kernel.Get<IUpdatesWindow>();
                updatesWindowController.SetState(true);
                
                _inputDispatcher = kernel.Get<IInputDispatcher>();

                _settingsForm.InitAura();
                _settingsForm.InitMatrix();

                var scheduler = kernel.Get<IScheduler>();
                _settingsForm.SetStartupCheck(scheduler.IsScheduled());


                SetAutoModes();

                // Subscribing for system power change events
                _powerlineStatusProvider.PowerlineStatusChanged += SystemEvents_PowerModeChanged;
                SystemEvents.UserPreferenceChanged += SystemEvents_UserPreferenceChanged;

                // Subscribing for monitor power on events
                _powerNotifier = kernel.Get<IPowerNotifier>();

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

                    /*
                    if (_settingsForm.UpdatesWindow is not null && _settingsForm.UpdatesWindow.Text != "")
                        _settingsForm.UpdatesWindow.InitTheme();
                        */

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

        private static void SystemEvents_PowerModeChanged(System.Windows.PowerLineStatus status)
        {
            Log.Debug("Power Mode Changed");
            SetAutoModes(true);
        }

        static void OnExit(object sender, EventArgs e)
        {
            _trayProvider.SetVisible(false);
            _powerNotifier.Dispose();
            Application.Exit();
        }
    }

}