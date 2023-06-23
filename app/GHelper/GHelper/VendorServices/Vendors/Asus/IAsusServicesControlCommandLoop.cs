namespace GHelper.VendorServices.Vendors.Asus;

public interface IAsusServicesControlCommandLoop
{
    public void Enqueue(IAsusServiceCommand command);
}