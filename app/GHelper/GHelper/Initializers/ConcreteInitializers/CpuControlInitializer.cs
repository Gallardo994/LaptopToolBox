using GHelper.DeviceControls.CPU;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

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