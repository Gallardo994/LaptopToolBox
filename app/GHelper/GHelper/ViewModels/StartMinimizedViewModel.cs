using GHelper.Configs;
using GHelper.Injection;
using Ninject;

namespace GHelper.ViewModels
{
    public sealed class StartMinimizedViewModel
    {
        public IConfig Config { get; } = Services.ResolutionRoot.Get<IConfig>();
        
        public StartMinimizedViewModel()
        {
            
        }
    }
}