using System;
using Microsoft.UI.Xaml;
using System.Reflection;
using System.Security.Principal;
using GHelper.Configs;
using GHelper.DeviceControls;
using GHelper.Helpers;
using GHelper.Initializers;
using GHelper.Injection;
using Ninject;
using Serilog;

namespace GHelper
{
    public partial class App
    {
        public App()
        {
            var appDataLogPath = System.IO.Path.Combine(ApplicationHelper.AppDataFolder, "log.txt");
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(appDataLogPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();
            
            UnhandledException += (sender, args) =>
            {
                Log.Error(args.Exception, "Unhandled exception");
            };
            
            if (!IsAdmin())
            {
                Log.Information("Running as non-admin, restarting as admin");
                Environment.Exit(0);
                return;
            }

            Log.Information("Running as admin");
            
            if (FocusSameInstance())
            {
                Log.Information("Another instance is running, exiting");
                Environment.Exit(0);
                return;
            }
            
            InitializeComponent();
        }
        
        private bool IsAdmin()
        {
            var osInfo = Environment.OSVersion;
            if (osInfo.Platform == PlatformID.Win32Windows)
            {
                return true;
            }
            else
            {
                var usrId = WindowsIdentity.GetCurrent();
                var p = new WindowsPrincipal(usrId);
                return p.IsInRole(@"BUILTIN\Administrators");
            }
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Log.Debug("Application launched");
            
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            
            Services.ResolutionRoot = kernel;

            kernel.Get<IConfig>().ReadFromLocalStorage();
            kernel.Get<IInitializersProvider>().InitializeAll();
        }
        
        private bool FocusSameInstance()
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var processes = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (var process in processes)
            {
                if (process.Id == currentProcess.Id)
                {
                    continue;
                }
                
                Native.SetForegroundWindow(process.MainWindowHandle);
                return true;
            }

            return false;
        }
    }
}
