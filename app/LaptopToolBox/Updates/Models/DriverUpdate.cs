﻿using CommunityToolkit.Mvvm.ComponentModel;

namespace LaptopToolBox.Updates.Models;

public partial class DriverUpdate : ObservableObject, IUpdate
{
    [ObservableProperty] private string _name;
    [ObservableProperty] private string _version;
    [ObservableProperty] private string _downloadUrl;
    [ObservableProperty] private bool _isNewerThanCurrent;
}