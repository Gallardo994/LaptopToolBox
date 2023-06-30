using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
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
