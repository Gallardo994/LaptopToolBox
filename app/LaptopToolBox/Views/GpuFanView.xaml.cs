using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace LaptopToolBox.Views
{
    public sealed partial class GpuFanView
    {
        public GpuFanViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<GpuFanViewModel>();
        
        public GpuFanView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
