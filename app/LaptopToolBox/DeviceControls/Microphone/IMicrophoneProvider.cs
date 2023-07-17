namespace LaptopToolBox.DeviceControls.Microphone;

public interface IMicrophoneProvider
{
    public void SetState(bool state);
    public bool IsMicrophoneEnabled();
}