using GHelper.Helpers;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;

namespace GHelper.ExtraControls.Wallpaper;

public interface IWallpaperProvider : IObservableObject
{
    public string ImagePath { get; }
    public BitmapImage ImageSource { get; }
}