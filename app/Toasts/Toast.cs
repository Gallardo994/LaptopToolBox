using Ninject;

namespace GHelper.Toasts;

public class Toast : IToast
{
    private readonly IToastIconResolver _iconResolver;
    
    [Inject]
    public Toast(IToastIconResolver iconResolver)
    {
        _iconResolver = iconResolver;
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
        
    }
}