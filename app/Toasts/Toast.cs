using Ninject;

namespace GHelper.Toasts;

public class Toast : IToast
{
    private readonly IToastIconResolver _iconResolver;
    private readonly IToastNativeWindow _toastWindow;

    [Inject]
    public Toast(IToastIconResolver iconResolver, IToastNativeWindow toastWindow)
    {
        _iconResolver = iconResolver;
        
        // TODO
        _toastWindow = toastWindow;
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