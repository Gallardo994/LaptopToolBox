using LaptopToolBox.AppUpdater;
using LaptopToolBox.AppUpdater.BackgroundWorkers;

namespace LaptopToolBox.Initializers.ConcreteInitializers;

public class BackgroundAppUpdateInitializer : IInitializer
{
    private readonly IBackgroundAppUpdateChecker _backgroundAppUpdateChecker;
    
    public BackgroundAppUpdateInitializer(IBackgroundAppUpdateChecker backgroundAppUpdateChecker)
    {
        _backgroundAppUpdateChecker = backgroundAppUpdateChecker;
    }
    
    public void Initialize()
    {
        _backgroundAppUpdateChecker.Start();
    }
}