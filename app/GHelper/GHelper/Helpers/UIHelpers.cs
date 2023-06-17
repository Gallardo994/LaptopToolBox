using Microsoft.UI.Xaml;

namespace GHelper.Helpers;

public class UiHelpers
{
    public static UIElement FindElementByName(UIElement element, string name)
    {
        if (element.XamlRoot == null || element.XamlRoot.Content == null)
        {
            return null;
        }
        var ele = (element.XamlRoot.Content as FrameworkElement)?.FindName(name);
        
        return ele as UIElement;
    }
}