using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class RamUsageView
    {
        public RamUsageViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<RamUsageViewModel>();
        
        public RamUsageView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
