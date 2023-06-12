using Ninject;

namespace GHelper.Toasts;

public class GlobalToastProvider : IGlobalToastProvider
{
    private readonly IToast _toast;
    
    [Inject]
    public GlobalToastProvider(IToast toast)
    {
        _toast = toast;
    }

    public void Dispose()
    {
        _toast.Dispose();
    }
}