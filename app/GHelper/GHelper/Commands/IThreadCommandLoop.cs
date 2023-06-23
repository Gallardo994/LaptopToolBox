using System;

namespace GHelper.Commands;

public interface IThreadCommandLoop : IDisposable
{
    public void Enqueue(IThreadCommand command);
}