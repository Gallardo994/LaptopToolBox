using System.Globalization;
using System.Threading;
using GHelper.AppConfigs;
using Ninject;
using Serilog;

namespace GHelper.Localization;

public class LanguageSetter : ILanguageSetter
{
    private readonly IAppConfig _appConfig;
    
    [Inject]
    public LanguageSetter(IAppConfig appConfig)
    {
        _appConfig = appConfig;
    }

    public void ResolveAndSetLanguage()
    {
        var language = _appConfig.Model.Language;

        if (!string.IsNullOrEmpty(language))
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        }
        else
        {
            var culture = CultureInfo.CurrentUICulture;
            if (culture.ToString() == "kr")
            {
                culture = CultureInfo.GetCultureInfo("ko");
            }
            
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        
        var newLanguage = CultureInfo.CurrentUICulture.ToString();
        
        if (newLanguage == language)
        {
            return;
        }
        
        Log.Information("Language changed from {OldLanguage} to {NewLanguage}", language, newLanguage);
        _appConfig.Model.Language = newLanguage;
        _appConfig.Save();
    }
}