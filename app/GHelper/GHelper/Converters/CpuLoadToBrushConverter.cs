using System;
using GHelper.Helpers;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media;

namespace GHelper.Converters;

public class CpuLoadToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is int intValue)
        {
            return intValue switch
            {
                >= 75 => new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0)),
                >= 50 => new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 127, 0)),
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