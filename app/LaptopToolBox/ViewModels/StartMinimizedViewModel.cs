using LaptopToolBox.Configs;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.ViewModels
{
    public sealed class StartMinimizedViewModel
    {
        public IConfig Config { get; } = Services.ResolutionRoot.Get<IConfig>();
        
        public StartMinimizedViewModel()
        {
            
        }
    }
}