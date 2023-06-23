namespace GHelper.VendorServices;

public interface IVendorServicesControl
{
    public int CountRunning();
    public void Enable();
    public void Disable();
}