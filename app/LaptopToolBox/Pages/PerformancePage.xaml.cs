using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Pages
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
