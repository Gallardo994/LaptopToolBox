using Microsoft.UI.Xaml;

namespace GHelper.Helpers;

public class ApplicationHelper
{
    public static string AppDataFolder => System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "GHelper");
    public static readonly string CurrentExecutableName = System.AppDomain.CurrentDomain.FriendlyName;

    public static void Exit()
    {
        Application.Current.Exit();
    }
}