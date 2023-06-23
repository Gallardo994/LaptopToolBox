using GHelper.Helpers;
using Microsoft.Win32;
using Serilog;

namespace GHelper.AutoStart;

public class AutoStartController : IAutoStartController
{
    private const string ApplicationName = "GHelper";

    private RegistryKey GetKey()
    {
        return Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
    } 
    
    private void EnableAutoStart()
    {
        if (IsAutoStartEnabled())
        {
            return;
        }

        using var key = GetKey();
        key.SetValue(ApplicationName, ApplicationHelper.CurrentExecutableName);
        
        Log.Information("AutoStartController.EnableAutoStart: Successfully created AutoStartPath: {AutoStartPath}", ApplicationHelper.CurrentExecutableName);
    }

    private void DisableAutoStart()
    {
        if (!IsAutoStartEnabled())
        {
            return;
        }
        
        using var key = GetKey();
        key.DeleteValue(ApplicationName);
    }
    
    public bool IsAutoStartEnabled()
    {
        using var key = GetKey();

        var autoStartPath = key.GetValue(ApplicationName) as string;
        return autoStartPath != null && autoStartPath.Equals(ApplicationHelper.CurrentExecutableName);
    }
    
    public void SetAutoStart(bool isAutoStartEnabled)
    {
        if (isAutoStartEnabled)
        {
            EnableAutoStart();
        }
        else
        {
            DisableAutoStart();
        }
    }
}