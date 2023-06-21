namespace GHelper.DeviceControls.CPU.Vendors.AMD;

public interface IAmdFamilyProvider
{
    public int FamilyId { get; }
    public string FamilyName { get; }
}