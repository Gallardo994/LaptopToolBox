using GHelper.Helpers;

namespace GHelper.ExtraControls.Wallpaper;

public interface IWallpaperProvider : IObservableObject
{
    public string ImagePath { get; }
}