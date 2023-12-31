﻿using System;
using System.Collections.ObjectModel;
using LaptopToolBox.Helpers;
using Microsoft.UI.Xaml.Controls;

namespace LaptopToolBox;

public interface IPageProvider : IObservableObject
{
    public ObservableCollection<FlyoutPageItem?> Pages { get; set; }
    public FlyoutPageItem? GetPageItem<T>() where T : Page;
    public FlyoutPageItem? GetPageItem(Type type);
    public FlyoutPageItem? GetPageItem(int index);
    public FlyoutPageItem GetHomePageItem();
    
    
    public ObservableCollection<FlyoutPageItem?> NormalPages { get; set; }
    public ObservableCollection<FlyoutPageItem?> FooterPages { get; set; }
}