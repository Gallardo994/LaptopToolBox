using Microsoft.UI.Xaml.Media;

namespace GHelper.Backdrops;

public interface IBackdropProvider
{
    public bool TryGetCompatibleBackdrop(out SystemBackdrop backdrop);
}