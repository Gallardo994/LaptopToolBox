using System.Collections.Generic;
using GHelper.DeviceControls.Battery;
using Ninject;

namespace GHelper.Initializers;

public class InitializersProvider : IInitializersProvider
{
    private readonly List<IInitializer> _initializers = new();

    [Inject]
    public InitializersProvider(
        BatteryInitializer batteryInitializer
        )
    {
        _initializers.Add(batteryInitializer);
    }
    
    public void InitializeAll()
    {
        foreach (var initializer in _initializers)
        {
            initializer.Initialize();
        }
    }
}