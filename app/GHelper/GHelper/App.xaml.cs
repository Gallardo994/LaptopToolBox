using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using System.Reflection;
using System.Security.Principal;
using GHelper.DeviceControls.CPU;
using GHelper.DeviceControls.Keyboard.Vendors;
using GHelper.Helpers;
using GHelper.Injection;
using Ninject;
using Serilog;

namespace GHelper
{
    public partial class App
    {
        private Window _window;
        
        public App()
        {
            var appDataLogPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "GHelper", "log.txt");
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(appDataLogPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();
            
            UnhandledException += (sender, args) =>
            {
                Log.Error(args.Exception, "Unhandled exception");
            };
            
            /*
            if (!IsAdmin())
            {
                Log.Information("Running as non-admin, restarting as admin");
                RunAsRestart();
                Environment.Exit(0);
            }
            else
            {
                Log.Information("Running as admin");
            }
            */
            
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
        
        private bool RunAsRestart()
        {
            var args = Environment.GetCommandLineArgs();

            foreach (var s in args)
            {
                if (s.Equals("runas"))
                {
                    return false;
                }
            }
            var startInfo = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = ApplicationHelper.CurrentExecutableName,
                Verb = "runas",
                Arguments = "runas"
            };

            try
            {
                Process.Start(startInfo);
            }
            catch (Exception e)
            {
                Log.Error(e, "Failed to restart as admin");
                return false;
            }
            return true;
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Log.Debug("Application launched");
            
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            
            Services.ResolutionRoot = kernel;

            kernel.Get<IVendorKeyRegister>();
            //kernel.Get<ICpuControl>();
            
            _window = kernel.Get<MainWindow>();
            
            // TODO: Minimize to tray support
            /*
            _window.Closed += (sender, windowArgs) =>
            {
                windowArgs.Handled = true;
            };
            */
            _window.Activate();
        }
    }
}
