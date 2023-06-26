namespace GHelper.DeviceControls.TouchPad;

public interface ITouchPadControl
{
    public bool IsAvailable { get; }
    public void SetState(bool state);
    public bool GetState();
}