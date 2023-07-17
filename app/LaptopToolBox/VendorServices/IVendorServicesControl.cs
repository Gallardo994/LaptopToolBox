namespace LaptopToolBox.VendorServices;

public interface IVendorServicesControl
{
    public int CountRunningSlow();
    public void Enable();
    public void Disable();
}