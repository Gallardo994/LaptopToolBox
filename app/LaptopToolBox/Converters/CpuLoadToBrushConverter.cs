using System;
using LaptopToolBox.Helpers;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace LaptopToolBox.Converters;

public class CpuLoadToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is int intValue)
        {
            return intValue switch
            {
                >= 75 => ColorBrushes.Critical,
                >= 50 => ColorBrushes.Caution,
                _ => ColorBrushes.Accent,
            };
        }

        return new SolidColorBrush(Windows.UI.Color.FromArgb(0, 0, 0, 0));
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}