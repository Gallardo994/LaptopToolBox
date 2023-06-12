namespace GHelper.AsusAcpi;

public interface IAsusAcpiProvider
{
    public bool TryGet(out AsusACPI? acpi);
}