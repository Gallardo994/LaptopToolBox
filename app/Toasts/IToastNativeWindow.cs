namespace GHelper.Toasts;

public interface IToastNativeWindow
{
    public void CreateHandle(CreateParams createParams);
    public void DestroyHandle();
}