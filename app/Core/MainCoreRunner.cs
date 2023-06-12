using System.Globalization;
using System.Reflection;
using GHelper.AsusAcpi;
using GHelper.Localization;
using Ninject;
using Serilog;

namespace GHelper.Core;

public class MainCoreRunner : ICoreRunner
{
    private readonly ILanguageSetter _languageSetter;
    private readonly IAsusAcpiErrorProvider _asusAcpiErrorProvider;
    private readonly IAsusAcpiProvider _asusAcpiProvider;
    
    [Inject]
    public MainCoreRunner(ILanguageSetter languageSetter, IAsusAcpiErrorProvider asusAcpiErrorProvider, IAsusAcpiProvider asusAcpiProvider)
    {
        _languageSetter = languageSetter;
        _asusAcpiErrorProvider = asusAcpiErrorProvider;
        _asusAcpiProvider = asusAcpiProvider;
    }

    public bool Run(string[] args)
    {
        _languageSetter.ResolveAndSetLanguage();
        
        ProcessHelper.CheckAlreadyRunning();
        
        if (!_asusAcpiErrorProvider.InvokeCheckAndNotify())
        {
            return false;
        }
                
        _asusAcpiProvider.TryGet(out Program.acpi);
        
        Log.Debug("------------");
        Log.Debug("App launched: " + AppConfig.GetModel() + " :" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + CultureInfo.CurrentUICulture + (ProcessHelper.IsUserAdministrator() ? "." : ""));

        Application.EnableVisualStyles();

        return true;
    }
}