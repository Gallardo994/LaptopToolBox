namespace GHelper.DeviceControls.CPU;

public interface ICpuDirectControl
{
    public bool IsUnderVoltSupported { get; }
    public void SetUnderVolt(int mv);
}