using Vanara.PInvoke;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusOpenWindowKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 56;

    public void Execute()
    {
        FocusThisApplication();
    }
    
    private void FocusThisApplication()
    {
        var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
        var processes = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
        foreach (var process in processes)
        {
            if (process.Id == currentProcess.Id)
            {
                User32.SetForegroundWindow(process.MainWindowHandle);
                User32.ShowWindow(process.MainWindowHandle, ShowWindowCommand.SW_RESTORE);
                return;
            }
        }
    }
}