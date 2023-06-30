using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
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
