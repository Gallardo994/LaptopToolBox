using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace GHelper.Helpers;

public class ColorBrushes
{
    public static SolidColorBrush Accent => (SolidColorBrush)Application.Current.Resources["AccentFillColorDefaultBrush"];
}