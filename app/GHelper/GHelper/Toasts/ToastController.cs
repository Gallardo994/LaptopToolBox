using Ninject;

namespace GHelper.Toasts;

public class ToastController : IToastController
{
    private readonly ToastWindow _toastWindow;
    
    [Inject]
    public ToastController(ToastWindow toastWindow)
    {
        _toastWindow = toastWindow;
        _toastWindow.Activated += (sender, args) =>
        {
            _toastWindow.HideOffScreen();
        };
        _toastWindow.Activate();
    }
    
    public void ShowToast(string title, string message)
    {
        _toastWindow.ShowMessage(title, message);
    }
}