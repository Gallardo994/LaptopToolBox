using LaptopToolBox.Updates.BackgroundWorkers;

namespace LaptopToolBox.Initializers.ConcreteInitializers;

public class BackgroundUpdatesInitializer : IInitializer
{
    private readonly IBackgroundUpdatesChecker _backgroundUpdatesChecker;
    
    public BackgroundUpdatesInitializer(IBackgroundUpdatesChecker backgroundUpdatesChecker)
    {
        _backgroundUpdatesChecker = backgroundUpdatesChecker;
    }
    
    public void Initialize()
    {
        _backgroundUpdatesChecker.Start();
    }
}