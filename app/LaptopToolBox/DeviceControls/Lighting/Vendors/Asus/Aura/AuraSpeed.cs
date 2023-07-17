namespace LaptopToolBox.DeviceControls.Lighting.Vendors.Asus.Aura;

public enum AuraSpeed : byte
{
    [SpeedTitle("Slow")] Slow = 0x01,
    [SpeedTitle("Normal")] Medium = 0xeb,
    [SpeedTitle("Fast")] Fast = 0xf5,
}