using System;
using System.Runtime.InteropServices;

namespace LaptopToolBox.Helpers.Native;

public class DwmApi
{
    [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
    public static extern void DwmSetWindowAttribute(IntPtr hwnd,
        DwmWindowAttribute attribute,
        ref DwmWindowCornerPreference pvAttribute,
        uint cbAttribute);
}