using Microsoft.UI.Xaml;
using System.Reflection;
using GHelper.Injection;
using Ninject;
using Serilog;

namespace GHelper
{
    public partial class App
    {
        public App()
        {
            var appDataLogPath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "GHelper", "log.txt");
            
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File(appDataLogPath, rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .CreateLogger();
            
            InitializeComponent();
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            Log.Debug("Application launched");
            
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            
            Services.ResolutionRoot = kernel;
            
            _window = kernel.Get<MainWindow>();
            _window.Activate();
        }

        private Window _window;
    }
}
