using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views
{
    public sealed partial class UnderVoltView
    {
        public UnderVoltViewModel ViewModel { get; private set; } = Services.ResolutionRoot.Get<UnderVoltViewModel>();
        
        public UnderVoltView()
        {
            InitializeComponent();
            DataContext = ViewModel;
        }
    }
}
