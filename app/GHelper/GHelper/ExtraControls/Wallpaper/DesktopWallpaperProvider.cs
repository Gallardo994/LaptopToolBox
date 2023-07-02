using System;
using System.IO;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;
using GHelper.Commands;
using GHelper.Helpers;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.Win32;
using Serilog;

namespace GHelper.ExtraControls.Wallpaper;

public partial class DesktopWallpaperProvider : ObservableObject, IWallpaperProvider
{
    [ObservableProperty] private string _imagePath;
    [ObservableProperty] private BitmapImage _imageSource;

    private readonly ISTACommandLoop _staCommandLoop;
    
    private const int TimerInterval = 5000;
    private readonly SafeTimer _timer;
    
    private long _imageSizeBytes;
    
    public DesktopWallpaperProvider(ISTACommandLoop staCommandLoop)
    {
        _staCommandLoop = staCommandLoop;

        ImagePath = GetImagePath();
        UpdateImageSource();
        
        _timer = new SafeTimer(TimerInterval);
        _timer.SafeElapsed += (sender, args) =>
        {
            _staCommandLoop.Enqueue(() =>
            {
                ImagePath = GetImagePath();
                UpdateImageSource();
            });
        };
        _timer.Start();
    }
    
    private bool IsImageSizeChanged()
    {
        var imageSizeBytes = new FileInfo(ImagePath).Length;
        var isImageSizeChanged = imageSizeBytes != _imageSizeBytes;
        _imageSizeBytes = imageSizeBytes;
        return isImageSizeChanged;
    }
    
    private void UpdateImageSource()
    {
        if (!IsImageSizeChanged())
        {
            return;
        }
        
        ImageSource = new BitmapImage(new Uri(ImagePath))
        {
            CreateOptions = BitmapCreateOptions.IgnoreImageCache,
        };
    }

    private string GetImagePath()
    {
        var pathWallpaper = string.Empty;
        
        using var regKey = Registry.CurrentUser.OpenSubKey("Control Panel\\Desktop", false);
        if (regKey == null)
        {
            return pathWallpaper;
        }
        
        pathWallpaper = regKey.GetValue("WallPaper")?.ToString();
        return pathWallpaper;
    }
}