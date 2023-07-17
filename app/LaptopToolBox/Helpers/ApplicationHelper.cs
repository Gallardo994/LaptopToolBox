using System.Diagnostics;
using Microsoft.UI.Xaml;

namespace LaptopToolBox.Helpers;

public class ApplicationHelper
{
    public static string AppDataFolder => System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), "LaptopToolBox");
    public static readonly string CurrentExecutableName = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.FriendlyName);

    public static void Exit()
    {
        Application.Current.Exit();
        Process.GetCurrentProcess().Kill();
    }
}