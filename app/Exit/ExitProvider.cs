namespace GHelper.Exit;

public class ExitProvider : IExitProvider
{
    public event Action OnExit;

    public ExitProvider()
    {
        AppDomain.CurrentDomain.ProcessExit += OnExitHandler;
    }
    
    private void OnExitHandler(object sender, EventArgs e)
    {
        OnExit?.Invoke();
    }
    
    public void Dispose()
    {
        AppDomain.CurrentDomain.ProcessExit -= OnExitHandler;
    }
}