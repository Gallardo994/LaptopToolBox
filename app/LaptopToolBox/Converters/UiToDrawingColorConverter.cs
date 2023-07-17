using System;
using Microsoft.UI.Xaml.Data;

namespace LaptopToolBox.Converters;

public class UiToDrawingColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var drawingColor = (System.Drawing.Color) value;
        return Windows.UI.Color.FromArgb(drawingColor.A, drawingColor.R, drawingColor.G, drawingColor.B);
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        var uiColor = (Windows.UI.Color) value;
        return System.Drawing.Color.FromArgb(uiColor.A, uiColor.R, uiColor.G, uiColor.B);
    }
}