using System;

namespace LaptopToolBox.Updates.Commands;

public class UpdatesActionCommand : IUpdatesCommand
{
    private readonly Action _action;
    
    public UpdatesActionCommand(Action action)
    {
        _action = action;
    }
    
    public void Execute()
    {
        _action();
    }
}