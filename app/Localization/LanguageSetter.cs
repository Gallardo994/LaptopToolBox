using System.Globalization;
using Serilog;

namespace GHelper.Localization;

public class LanguageSetter : ILanguageSetter
{
    public void ResolveAndSetLanguage()
    {
        var language = AppConfig.GetString("language"); // TODO: Move to IAppConfig

        if (language != null && language.Length > 0)
        {
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        }
        else
        {
            var culture = CultureInfo.CurrentUICulture;
            if (culture.ToString() == "kr") culture = CultureInfo.GetCultureInfo("ko");
            Thread.CurrentThread.CurrentUICulture = culture;
        }

        Log.Debug(CultureInfo.CurrentUICulture.ToString());
    }
}