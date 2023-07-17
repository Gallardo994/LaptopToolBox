using System;
using Microsoft.UI.Xaml.Data;

namespace LaptopToolBox.Converters;

public class GreaterThanZeroToBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value is int intValue && intValue > 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}