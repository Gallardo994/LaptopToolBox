using GHelper.DeviceControls.Keyboard.Vendors;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

public class VendorKeyRegisterInitializer : IInitializer
{
    private readonly IVendorKeyRegister _vendorKeyRegister;
    
    [Inject]
    public VendorKeyRegisterInitializer(IVendorKeyRegister vendorKeyRegister)
    {
        _vendorKeyRegister = vendorKeyRegister;
    }
    
    public void Initialize()
    {
        _vendorKeyRegister.Register();
    }
}