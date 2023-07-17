using System;

namespace LaptopToolBox.Commands;

public interface ISTACommandLoop
{
    public void Enqueue(ISTACommand command);
    public void Enqueue(Action action);
}