using LaptopToolBox.DeviceControls.CPU;
using Ninject;

namespace LaptopToolBox.Initializers.ConcreteInitializers;

public class CpuControlInitializer : IInitializer
{
    private readonly ICpuControl _cpuControl;
    
    [Inject]
    public CpuControlInitializer(ICpuControl cpuControl)
    {
        _cpuControl = cpuControl;
    }
    
    public void Initialize()
    {
        // Early injection to avoid UI lag when user opens Performance tab
    }
}