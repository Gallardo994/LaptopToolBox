using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;

namespace LaptopToolBox.Helpers;

public class ColorBrushes
{
    public static SolidColorBrush Accent => (SolidColorBrush)Application.Current.Resources["AccentFillColorDefaultBrush"];
    public static SolidColorBrush Caution => (SolidColorBrush)Application.Current.Resources["SystemFillColorCautionBrush"];
    public static SolidColorBrush Critical => (SolidColorBrush)Application.Current.Resources["SystemFillColorCriticalBrush"];
    public static SolidColorBrush Neutral => (SolidColorBrush)Application.Current.Resources["SystemFillColorNeutralBrush"];
    
}