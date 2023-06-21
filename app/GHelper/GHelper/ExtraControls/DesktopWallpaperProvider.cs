using Microsoft.Win32;

namespace GHelper.ExtraControls;

public class DesktopWallpaperProvider : IWallpaperProvider
{
    public string GetImagePath()
    {
        var pathWallpaper = string.Empty;
        
        var regKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
        if (regKey == null)
        {
            return pathWallpaper;
        }
        
        pathWallpaper = regKey.GetValue("WallPaper")?.ToString();
        regKey.Close();
        return pathWallpaper;
    }
}