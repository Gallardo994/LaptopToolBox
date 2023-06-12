using Ninject;

namespace GHelper.Toasts;

public class Toast : IToast
{
    private readonly IToastIconResolver _iconResolver;
    private readonly NativeWindow _toastWindow;

    [Inject]
    public Toast(IToastIconResolver iconResolver)
    {
        _iconResolver = iconResolver;
        
        _toastWindow = new NativeWindow();
        
        // TODO
        _toastWindow.CreateHandle(new CreateParams());
    }

    public void Show(string text, ToastIconType iconType)
    {
        var iconBitmap = _iconResolver.ResolveIcon(iconType);
    }

    public void Hide()
    {
        
    }

    public void Dispose()
    {
        _toastWindow.DestroyHandle();
    }
}