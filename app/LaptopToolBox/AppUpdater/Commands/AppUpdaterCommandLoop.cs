using System;
using LaptopToolBox.Commands;

namespace LaptopToolBox.AppUpdater.Commands;

public class AppUpdaterCommandLoop : BackgroundCommandLoop<IAppUpdaterCommand>, IAppUpdaterCommandLoop
{
    public void Enqueue(Action action)
    {
        Enqueue(new AppUpdaterCommand(action));
    }
}