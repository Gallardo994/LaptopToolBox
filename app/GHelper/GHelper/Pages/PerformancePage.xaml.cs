using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Pages
{
    public sealed partial class PerformancePage
    {
        public PerformanceViewModel PerformanceViewModel { get; } = Services.ResolutionRoot.Get<PerformanceViewModel>();
        
        public PerformancePage()
        {
            InitializeComponent();
            
            DataContext = PerformanceViewModel;
        }
    }
}
