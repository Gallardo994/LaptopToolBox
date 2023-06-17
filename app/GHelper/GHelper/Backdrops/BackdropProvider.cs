using Microsoft.UI.Composition.SystemBackdrops;
using Microsoft.UI.Xaml.Media;

namespace GHelper.Backdrops;

public class BackdropProvider : IBackdropProvider
{
    public bool TryGetCompatibleBackdrop(out SystemBackdrop backdrop)
    {
        backdrop = default;
        
        if (MicaController.IsSupported())
        {
            backdrop = new MicaBackdrop();
            return true;
        }
        
        if (DesktopAcrylicController.IsSupported())
        {
            backdrop = new DesktopAcrylicBackdrop();
            return true;
        }

        return false;
    }
}