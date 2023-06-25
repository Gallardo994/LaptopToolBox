using Microsoft.UI.Xaml;
using Vanara.PInvoke;

namespace GHelper.Helpers;

public static class WindowExtensions
{
    public static Window Show(this Window window)
    {
        WindowHelper.ShowWindow(window, ShowWindowCommand.SW_SHOWNORMAL);
        return window;
    }
    
    public static Window Hide(this Window window)
    {
        WindowHelper.ShowWindow(window, ShowWindowCommand.SW_HIDE);
        return window;
    }
    
    public static Window Focus(this Window window)
    {
        WindowHelper.FocusWindow(window);
        return window;
    }
    
    public static Window Restore(this Window window)
    {
        WindowHelper.ShowWindow(window, ShowWindowCommand.SW_RESTORE);
        return window;
    }
}