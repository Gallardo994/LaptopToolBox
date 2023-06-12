namespace GHelper.Powerline;

public interface IPowerlineStatusProvider : IDisposable
{
    public event Action<PowerLineStatus> PowerlineStatusChanged;
    public PowerLineStatus IsPlugged { get; }
}