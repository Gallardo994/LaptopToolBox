using System;
using System.Collections.Generic;
using LaptopToolBox.IPC.Messages;

namespace LaptopToolBox.IPC;

public class IpcResolver : IIpcResolver
{
    private readonly Dictionary<long, Type> _idToType;
    private readonly Dictionary<Type, long> _typeToId;

    public IpcResolver()
    {
        _idToType = new Dictionary<long, Type>();
        _typeToId = new Dictionary<Type, long>();
        
        Register<IpcBringToFront>();
    }

    public void Register<T>() where T : class, IIpcMessage, new()
    {
        var type = typeof(T);
        var id = new T().Id;
        
        _idToType.Add(id, type);
        _typeToId.Add(type, id);
    }
    
    public long GetId<T>() where T : class, IIpcMessage
    {
        return _typeToId[typeof(T)];
    }
    
    public Type GetType(long id)
    {
        return _idToType[id];
    }
    
    public bool TryGetType(long id, out Type type)
    {
        return _idToType.TryGetValue(id, out type);
    }
}