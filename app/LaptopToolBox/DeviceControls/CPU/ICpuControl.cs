namespace LaptopToolBox.DeviceControls.CPU;

public interface ICpuControl
{
    public bool IsUnderVoltSupported { get; }
    public void SetUnderVolt(int mv);
}