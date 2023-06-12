using System.Globalization;
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

        Log.Debug(CultureInfo.CurrentUICulture.ToString());
    }
}