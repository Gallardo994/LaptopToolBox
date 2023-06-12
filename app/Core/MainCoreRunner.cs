using System.Globalization;
using System.Reflection;
using GHelper.AsusAcpi;
using GHelper.Localization;
using GHelper.ProcessHelpers;
using Ninject;
using Serilog;

namespace GHelper.Core;

public class MainCoreRunner : ICoreRunner
{
    private readonly ILanguageSetter _languageSetter;
    private readonly IAsusAcpiErrorProvider _asusAcpiErrorProvider;
    private readonly IAsusAcpiProvider _asusAcpiProvider;
    private readonly IAdministratorHelper _administratorHelper;
    
    [Inject]
    public MainCoreRunner(ILanguageSetter languageSetter, 
        IAsusAcpiErrorProvider asusAcpiErrorProvider, 
        IAsusAcpiProvider asusAcpiProvider,
        IAdministratorHelper administratorHelper)
    {
        _languageSetter = languageSetter;
        _asusAcpiErrorProvider = asusAcpiErrorProvider;
        _asusAcpiProvider = asusAcpiProvider;
        _administratorHelper = administratorHelper;
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
        Log.Debug("App launched: " + AppConfig.GetModel() + " :" + Assembly.GetExecutingAssembly().GetName().Version.ToString() + CultureInfo.CurrentUICulture + (_administratorHelper.IsUserAdministrator() ? "." : ""));

        Application.EnableVisualStyles();
        
        HardwareControl.RecreateGpuControl();

        return true;
    }
}