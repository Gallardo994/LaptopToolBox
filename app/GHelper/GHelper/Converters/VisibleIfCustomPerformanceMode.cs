using System;
using GHelper.DeviceControls.PerformanceModes;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Data;

namespace GHelper.Converters;

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