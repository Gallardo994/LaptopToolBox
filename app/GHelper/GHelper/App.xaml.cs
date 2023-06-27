using System;
using System.Diagnostics;
using Microsoft.UI.Xaml;
using System.Reflection;
using System.Security.Principal;
using GHelper.Configs;
using GHelper.Helpers;
using GHelper.Initializers;
using GHelper.Injection;
using GHelper.IPC.Messages;
using GHelper.IPC.Publishers;
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
                .WriteTo.File(appDataLogPath, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 10, retainedFileTimeLimit: TimeSpan.FromDays(3))
                .WriteTo.Console()
                .MinimumLevel.Debug()
                .CreateLogger();
            
            UnhandledException += (sender, args) =>
            {
                Log.Error(args.Exception, "Unhandled exception");
            };
            
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            
            Services.ResolutionRoot = kernel;
            
            if (!IsAdmin())
            {
                Log.Information("Running as non-admin, restarting as admin");
                ApplicationHelper.Exit();
                return;
            }

            Log.Information("Running as admin");
            
            if (FocusSameInstance())
            {
                Log.Information("Another instance is running, exiting");
                ApplicationHelper.Exit();
                return;
            }
            
            InitializeComponent();
        }

        private bool IsAdmin()
        {
            var identity = WindowsIdentity.GetCurrent();
            var principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Log.Debug("Application launched");

            Services.ResolutionRoot.Get<IConfig>().ReadFromLocalStorage();
            Services.ResolutionRoot.Get<IInitializersProvider>().InitializeAll();
        }
        
        private bool FocusSameInstance()
        {
            var currentProcess = Process.GetCurrentProcess();
            
            var processes = Process.GetProcessesByName(currentProcess.ProcessName);
            Log.Debug("Found {ProcessCount} processes with name {ProcessName}", processes.Length, currentProcess.ProcessName);
            
            foreach (var process in processes)
            {
                if (process.Id == currentProcess.Id)
                {
                    continue;
                }

                try
                {
                    var publisher = new IpcPublisher(process.Id);
                    var result = publisher.Publish(new IpcBringToFront());
                
                    Log.Debug("Published {Message} to process {ProcessId}, result: {Result}", typeof(IpcBringToFront), process.Id, result);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Failed to publish {Message} to process {ProcessId}", typeof(IpcBringToFront), process.Id);
                }
            }
            
            return processes.Length > 1;
        }
    }
}
