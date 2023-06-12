namespace GHelper.AsusAcpi;

public class AsusAcpiProvider : IAsusAcpiProvider
{
    private readonly AsusACPI? _acpi;

    public AsusAcpiProvider()
    {
        try
        {
            _acpi = new AsusACPI();
        }
        catch (Exception)
        {
            _acpi = null;
        }
    }
    
    public bool TryGet(out AsusACPI? acpi)
    {
        acpi = _acpi;
        return acpi != null;
    }
}