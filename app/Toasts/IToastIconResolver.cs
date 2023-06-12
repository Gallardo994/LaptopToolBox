namespace GHelper.Toasts;

public interface IToastIconResolver
{
    public Bitmap ResolveIcon(ToastIconType toastIconType);
}