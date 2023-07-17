using System;

namespace LaptopToolBox.Commands;

public class ActionCommand : IBackgroundCommand, ISTACommand
{
    private readonly Action _action;
    
    public ActionCommand(Action action)
    {
        _action = action;
    }
    
    public void Execute()
    {
        _action();
    }
}