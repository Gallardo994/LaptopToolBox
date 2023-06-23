using Microsoft.UI.Xaml;

namespace GHelper.Helpers;

public static class WindowExtensions
{
    public static Window Show(this Window window)
    {
        WindowHelper.ShowWindow(window);
        return window;
    }
    
    public static Window Hide(this Window window)
    {
        WindowHelper.HideWindow(window);
        return window;
    }
    
    public static Window Focus(this Window window)
    {
        WindowHelper.FocusWindow(window);
        return window;
    }
}