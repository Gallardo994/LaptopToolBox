namespace GHelper.Updates;

public interface IModelInfoProvider
{
    public string Model { get; }
    public string Bios { get; }
    public int GetNumericBiosVersion();
}