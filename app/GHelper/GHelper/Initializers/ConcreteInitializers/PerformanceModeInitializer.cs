using GHelper.DeviceControls.PerformanceModes;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

public class PerformanceModeInitializer : IInitializer
{
    private readonly IPerformanceModeControl _performanceModeControl;
    
    [Inject]
    public PerformanceModeInitializer(IPerformanceModeControl performanceModeControl)
    {
        _performanceModeControl = performanceModeControl;
    }
    
    public void Initialize()
    {
        _performanceModeControl.SetMode(_performanceModeControl.GetCurrentMode());
    }
}