using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;
using Ninject.Parameters;

namespace GHelper.Views
{
    public sealed partial class ModifyPerformanceProfileView
    {
        public ModifyPerformanceProfileViewModel ViewModel { get; set; }

        public ModifyPerformanceProfileView(CustomPerformanceMode performanceMode)
        {
            ViewModel = Services.ResolutionRoot.Get<ModifyPerformanceProfileViewModel>(new ConstructorArgument("performanceMode", performanceMode));
            
            InitializeComponent();
            
            
        }
    }
}
