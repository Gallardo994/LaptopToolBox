using System;

namespace LaptopToolBox.IPC;

public interface IIpcResolver
{
    public void Register<T>() where T : class, IIpcMessage, new();
    public long GetId<T>() where T : class, IIpcMessage;
    public Type GetType(long id);
    public bool TryGetType(long id, out Type type);
}