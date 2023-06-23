using System;
using GHelper.Helpers.Native;
using Vanara.PInvoke;

namespace GHelper.Helpers;

public class WindowHelper
{
    public static void SetRoundedCorners(IntPtr handle, DwmWindowCornerPreference preference)
    {
        DwmApi.DwmSetWindowAttribute(
            handle,
            DwmWindowAttribute.DwmwaWindowCornerPreference,
            ref preference,
            sizeof(uint));
    }

    public static void SetTransparent(IntPtr handle, bool state)
    {
        var extendedStyle = User32.GetWindowLong(handle, User32.WindowLongFlags.GWL_EXSTYLE);
    
        if (state)
        {
            User32.SetWindowLong(handle, User32.WindowLongFlags.GWL_EXSTYLE, extendedStyle | 0x80000);
        }
        else
        {
            User32.SetWindowLong(handle, User32.WindowLongFlags.GWL_EXSTYLE, extendedStyle & ~0x80000);
        }
    }
}