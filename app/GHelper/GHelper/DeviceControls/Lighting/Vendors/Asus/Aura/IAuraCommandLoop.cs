namespace GHelper.DeviceControls.Lighting.Vendors.Asus.Aura;

public interface IAuraCommandLoop
{
    public void Enqueue(AuraApplyCommand command);
}