using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class CpuFanView
    {
        public CpuFanViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<CpuFanViewModel>();
        
        public CpuFanView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
