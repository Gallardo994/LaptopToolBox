using GHelper.AppUpdater;
using GHelper.AppUpdater.BackgroundWorkers;

namespace GHelper.Initializers.ConcreteInitializers;

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