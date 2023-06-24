namespace GHelper.DeviceControls.Microphone;

public interface IMicrophoneProvider
{
    public void SetState(bool state);
    public bool IsMicrophoneEnabled();
}