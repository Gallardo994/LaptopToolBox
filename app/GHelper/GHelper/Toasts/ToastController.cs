namespace GHelper.Toasts;

public class ToastController : IToastController
{
    public void ShowToast(string message)
    {
        var toastWindow = new ToastWindow();
        toastWindow.Activate();
    }
}