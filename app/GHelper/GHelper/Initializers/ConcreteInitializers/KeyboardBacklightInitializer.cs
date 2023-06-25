using GHelper.Configs;
using GHelper.DeviceControls.Lighting;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

public class KeyboardBacklightInitializer : IInitializer
{
    private readonly IConfig _config;
    private readonly IVendorKeyboardBacklightController _vendorKeyboardBacklightController;
    
    [Inject]
    public KeyboardBacklightInitializer(IConfig config, IVendorKeyboardBacklightController vendorKeyboardBacklightController)
    {
        _config = config;
        _vendorKeyboardBacklightController = vendorKeyboardBacklightController;
    }
    
    public void Initialize()
    {
        _vendorKeyboardBacklightController.SetBrightness(_config.KeyboardBacklightBrightness);
    }
}