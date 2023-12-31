﻿using LaptopToolBox.Helpers;

namespace LaptopToolBox.AppUpdater;

public interface IAppUpdateProvider : IObservableObject
{
    public void CheckForUpdate();
    public void InstallUpdate();
    
    public bool IsUpdateBusy { get; }
    public bool IsCheckingForUpdates { get; }
    
    public bool IsDownloadingUpdate { get; }
    public bool IsUpdateInstallAvailable { get; }
}