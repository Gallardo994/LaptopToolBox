namespace GHelper.DeviceControls.CPU;

public interface ICpuFamilyProvider
{
    public int FamilyId { get; }
    public string FamilyName { get; }
}