using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
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
