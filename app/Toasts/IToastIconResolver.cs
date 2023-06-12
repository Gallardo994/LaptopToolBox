namespace GHelper.Toasts;

public interface IToastIconResolver
{
    public Bitmap ResolveIcon(ToastIcon toastIcon);
}