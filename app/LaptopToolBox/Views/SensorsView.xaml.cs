using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class SensorsView
    {
        public SensorsViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<SensorsViewModel>();
        
        public SensorsView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
