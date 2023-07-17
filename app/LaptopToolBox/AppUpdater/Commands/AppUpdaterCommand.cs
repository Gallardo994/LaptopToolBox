using System;

namespace LaptopToolBox.AppUpdater.Commands;

public class AppUpdaterCommand : IAppUpdaterCommand
{
    private readonly Action _action;
    
    public AppUpdaterCommand(Action action)
    {
        _action = action;
    }
    
    public void Execute()
    {
        _action();
    }
}