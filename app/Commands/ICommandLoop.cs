using System;

namespace GHelper.Commands;

public interface ICommandLoop : IDisposable
{
    public void Add(ICommand command);
}