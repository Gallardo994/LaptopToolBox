using System.Globalization;
using GHelper.Resources.Strings;

namespace GHelper.Helpers;

public class Loc : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var str = AppResources.ResourceManager.GetString(parameter.ToString()!)!;
        
        return parameter == null ? str : string.Format(str, value);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}