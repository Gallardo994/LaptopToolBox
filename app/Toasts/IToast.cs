namespace GHelper.Toasts;

public interface IToast : IDisposable
{
    public void Show();
    public void Hide();
}