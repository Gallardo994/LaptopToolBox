using System.Diagnostics;
using GHelper.Execute;

namespace GHelper.AsusAcpi;

public class AsusAcpiErrorProvider : IAsusAcpiErrorProvider
{
    private readonly IAsusAcpiProvider _provider;
    private readonly IShellRunner _shellRunner;
    
    public AsusAcpiErrorProvider(IAsusAcpiProvider provider, IShellRunner shellRunner)
    {
        _provider = provider;
        _shellRunner = shellRunner;
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
            _shellRunner.Execute("https://www.asus.com/support/FAQ/1047338/");
        }

        Application.Exit();

        return false;
    }
}