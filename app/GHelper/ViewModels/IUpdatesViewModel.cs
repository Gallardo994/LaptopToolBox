﻿using System.Collections.ObjectModel;
using GHelper.Updates.Models;

namespace GHelper.ViewModels;

public interface IUpdatesViewModel
{
    public bool IsUpdating { get; set; }
    public int PendingUpdates { get; set; }
    public ObservableCollection<IUpdate> Updates { get; set; }
    
    public void SetUpdates(List<IUpdate> updates);
}