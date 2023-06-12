using System.Collections.Concurrent;

namespace GHelper.Commands;

public class CommandLoop : ICommandLoop
{
    private readonly ConcurrentQueue<ICommand> _commands;
    
    public CommandLoop()
    {
        _commands = new ConcurrentQueue<ICommand>();
    }
    
    public void Add(ICommand command)
    {
        _commands.Enqueue(command);
    }
    
    public void Run()
    {
        while (_commands.TryDequeue(out var command))
        {
            command.Execute();
        }
    }
}