using System;

namespace GHelper.Commands;

public interface ICommandLoop : IDisposable
{
    public void Enqueue(ICommand command);
}