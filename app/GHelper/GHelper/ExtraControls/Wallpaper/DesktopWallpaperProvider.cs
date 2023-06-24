using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Win32;

namespace GHelper.ExtraControls.Wallpaper;

public partial class DesktopWallpaperProvider : ObservableObject, IWallpaperProvider
{
    [ObservableProperty] private string _imagePath;
    
    public DesktopWallpaperProvider()
    {
        ImagePath = GetImagePath();
    }
    
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