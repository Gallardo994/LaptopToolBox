using System.Diagnostics;

namespace GHelper.AsusAcpi;

public class AsusAcpiErrorProvider : IAsusAcpiErrorProvider
{
    private readonly IAsusAcpiProvider _provider;
    
    public AsusAcpiErrorProvider(IAsusAcpiProvider provider)
    {
        _provider = provider;
    }
    
    public bool InvokeCheckAndNotify()
    {
        if (_provider.TryGet(out _))
        {
            return true;
        }
        
        DialogResult dialogResult = MessageBox.Show(Properties.Strings.ACPIError, Properties.Strings.StartupError, MessageBoxButtons.YesNo);
        if (dialogResult == DialogResult.Yes)
        {
            Process.Start(new ProcessStartInfo("https://www.asus.com/support/FAQ/1047338/") { UseShellExecute = true });
        }

        Application.Exit();

        return false;
    }
}