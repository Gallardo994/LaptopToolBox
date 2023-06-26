namespace GHelper.DeviceControls.Display.Brightness;

public interface IBrightnessControl
{
    public int BrightnessStep { get; set; }
    public int Get();
    public void Set(int brightness);
    public void StepUp();
    public void StepDown();
}