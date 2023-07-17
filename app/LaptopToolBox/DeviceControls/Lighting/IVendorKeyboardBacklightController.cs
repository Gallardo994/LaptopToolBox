namespace LaptopToolBox.DeviceControls.Lighting;

public interface IVendorKeyboardBacklightController
{
    public void SetBrightness(byte brightness);
    public void IncrementBrightness();
    public void DecrementBrightness();
}