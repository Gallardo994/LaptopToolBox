using System;
using System.Collections.Generic;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

namespace GHelper.Helpers;

public static class WindowHelpers
{
    public static List<Window> ActiveWindows { get; } = new List<Window>();

    public static void TrackWindow(Window window)
    {
        window.Closed += (sender, args) => { ActiveWindows.Remove(window); };
        ActiveWindows.Add(window);
    }

    public static AppWindow GetAppWindow(Window window)
    {
        var hWnd = WindowNative.GetWindowHandle(window);
        var wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        return AppWindow.GetFromWindowId(wndId);
    }

    public static Window GetWindowForElement(UIElement element)
    {
        if (element.XamlRoot == null)
        {
            return null;
        }
        
        foreach (var window in ActiveWindows)
        {
            if (element.XamlRoot == window.Content.XamlRoot)
            {
                return window;
            }
        }

        return null;
    }
}