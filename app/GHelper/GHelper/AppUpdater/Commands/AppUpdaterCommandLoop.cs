using System;
using GHelper.Commands;

namespace GHelper.AppUpdater.Commands;

public class AppUpdaterCommandLoop : BackgroundCommandLoop<IAppUpdaterCommand>, IAppUpdaterCommandLoop
{
    public void Enqueue(Action action)
    {
        Enqueue(new AppUpdaterCommand(action));
    }
}