using System;

namespace LaptopToolBox.Commands;

public interface IBackgroundCommandLoop : IDisposable
{
    public void Enqueue(IBackgroundCommand command);
    public void Enqueue(Action action);
}