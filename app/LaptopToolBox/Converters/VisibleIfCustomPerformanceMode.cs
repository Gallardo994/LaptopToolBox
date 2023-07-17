using System;
using LaptopToolBox.DeviceControls.PerformanceModes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace LaptopToolBox.Converters;

public class VisibleIfCustomPerformanceMode : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        return value is CustomPerformanceMode ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}