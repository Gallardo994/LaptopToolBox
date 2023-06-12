namespace GHelper.Toasts;

public interface IToast : IDisposable
{
    public void Show(string text, ToastIconType iconType);
    public void Hide();
}