using System.Collections.Generic;
using GHelper.Initializers.ConcreteInitializers;
using Ninject;

namespace GHelper.Initializers;

public class InitializersProvider : IInitializersProvider
{
    private readonly List<IInitializer> _initializers = new();

    [Inject]
    public InitializersProvider(
        BatteryInitializer batteryInitializer,
        VendorKeyRegisterInitializer vendorKeyRegisterInitializer,
        CpuControlInitializer cpuControlInitializer
        )
    {
        _initializers.Add(batteryInitializer);
        _initializers.Add(vendorKeyRegisterInitializer);
        _initializers.Add(cpuControlInitializer);
    }
    
    public void InitializeAll()
    {
        foreach (var initializer in _initializers)
        {
            initializer.Initialize();
        }
    }
}