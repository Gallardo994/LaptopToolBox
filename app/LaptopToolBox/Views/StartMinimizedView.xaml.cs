using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class StartMinimizedView
    {
        public StartMinimizedViewModel ViewModel { get; } = Services.ResolutionRoot.Get<StartMinimizedViewModel>();
        
        public StartMinimizedView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
