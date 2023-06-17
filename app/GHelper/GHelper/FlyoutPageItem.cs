using System;
using Microsoft.UI.Xaml.Controls;

namespace GHelper;

public class FlyoutPageItem
{
    public string Title { get; set; }
    public Type TargetType { get; set; }
    public BitmapIcon Icon { get; set; }
    public bool IsHomePage { get; set; }
    public string Tag { get; set; }
}