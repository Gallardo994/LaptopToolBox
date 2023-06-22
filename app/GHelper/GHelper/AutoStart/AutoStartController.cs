using System;
using System.IO;
using GHelper.Helpers;

namespace GHelper.AutoStart;

public class AutoStartController : IAutoStartController
{
    private string AutoStartPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), "GHelper.lnk");

    private void EnableAutoStart()
    {
        if (IsAutoStartEnabled())
        {
            return;
        }
        
        using var writer = new StreamWriter(AutoStartPath);
        writer.WriteLine("[InternetShortcut]");
        writer.WriteLine("URL=file:///" + ApplicationHelper.CurrentExecutableName);
        writer.WriteLine("IconIndex=0");
        writer.WriteLine("IconFile=" + ApplicationHelper.CurrentExecutableName);
        writer.Flush();
    }

    private void DisableAutoStart()
    {
        if (!IsAutoStartEnabled())
        {
            return;
        }
        
        try
        {
            File.Delete(AutoStartPath);
        } 
        catch
        {
            // Ignore
        }
    }
    
    public bool IsAutoStartEnabled()
    {
        if (!File.Exists(AutoStartPath))
        {
            return false;
        }
        
        using var reader = new StreamReader(AutoStartPath);
            
        while (reader.ReadLine() is { } line)
        {
            if (line.StartsWith("URL=file:///") && line.EndsWith(ApplicationHelper.CurrentExecutableName))
            {
                return true;
            }
        }

        return false;
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