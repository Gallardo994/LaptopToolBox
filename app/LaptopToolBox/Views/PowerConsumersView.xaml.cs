using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class PowerConsumersView
    {
        public PowerConsumersViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<PowerConsumersViewModel>();
        
        public PowerConsumersView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
