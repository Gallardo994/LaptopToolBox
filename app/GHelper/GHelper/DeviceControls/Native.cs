using System;
using System.Runtime.InteropServices;

namespace GHelper.DeviceControls;

public static class Native
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool DeviceIoControl(
        IntPtr hDevice,
        uint dwIoControlCode,
        byte[] lpInBuffer,
        uint nInBufferSize,
        byte[] lpOutBuffer,
        uint nOutBufferSize,
        ref uint lpBytesReturned,
        IntPtr lpOverlapped
    );
    
    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    public static extern IntPtr CreateFile(
        string lpFileName,
        uint dwDesiredAccess,
        uint dwShareMode,
        IntPtr lpSecurityAttributes,
        uint dwCreationDisposition,
        uint dwFlagsAndAttributes,
        IntPtr hTemplateFile
    );
    
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool CloseHandle(IntPtr hObject);
    
    [DllImport("kernel32")]
    public static extern IntPtr LoadLibrary(string lpFileName);

    [DllImport("kernel32", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = false)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);
    
    [DllImport("inpoutx64.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool GetPhysLong(UIntPtr memAddress, out uint Data);
    
    [DllImport("user32.dll")]
    public static extern bool SetForegroundWindow(IntPtr hWnd);
    
    [DllImport("user32.dll")]
    public static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey,byte bAlpha, uint dwFlags);
    
    [DllImport("user32.dll")]
    public static extern int SetWindowLong(IntPtr hWnd, int nIndex, uint dwNewLong);
    
    [DllImport("user32.dll", SetLastError = true)]
    public static extern uint GetWindowLong(IntPtr hWnd, int nIndex);
}