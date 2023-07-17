namespace LaptopToolBox.AlwaysAwake;

public interface IAlwaysAwakeController
{
    public void Start();
    public void Stop();
    public bool IsRunning { get; }
}