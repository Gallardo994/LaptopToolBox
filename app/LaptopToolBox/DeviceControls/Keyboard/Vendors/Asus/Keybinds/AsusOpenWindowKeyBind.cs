using LaptopToolBox.Helpers;
using LaptopToolBox.AppWindows;
using LaptopToolBox.Injection;
using Ninject;

namespace LaptopToolBox.DeviceControls.Keyboard.Vendors.Asus.Keybinds;

public class AsusOpenWindowKeyBind : IVendorKeyBind
{
    public int Key { get; set; } = 56;

    public void Execute()
    {
        Services.ResolutionRoot.Get<MainWindow>().BringToFront();
    }
}