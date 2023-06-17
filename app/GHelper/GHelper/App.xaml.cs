using Microsoft.UI.Xaml;
using System.Reflection;
using GHelper.Injection;
using Ninject;

namespace GHelper
{
    public partial class App
    {
        public App()
        {
            InitializeComponent();
        }
        
        protected override void OnLaunched(LaunchActivatedEventArgs args)
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            
            Services.ResolutionRoot = kernel;
            
            _window = kernel.Get<MainWindow>();
            _window.Activate();
        }

        private Window _window;
    }
}
