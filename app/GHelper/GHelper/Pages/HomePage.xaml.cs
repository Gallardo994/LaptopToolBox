using GHelper.AppUpdater;
using GHelper.Injection;
using Ninject;

namespace GHelper.Pages
{
    public sealed partial class HomePage
    {
        private readonly IAppUpdater _appUpdater = Services.ResolutionRoot.Get<IAppUpdater>();
        
        public HomePage()
        {
            InitializeComponent();
        }
    }
}
