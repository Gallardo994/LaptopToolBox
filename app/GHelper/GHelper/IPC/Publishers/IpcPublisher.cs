using System;
using Cloudtoid.Interprocess;
using GHelper.Injection;
using GHelper.Serialization;
using Ninject;
using Serilog;

namespace GHelper.IPC.Publishers;

public class IpcPublisher
{
    private readonly IIpcResolver _resolver = Services.ResolutionRoot.Get<IIpcResolver>();
    private readonly IPublisher _publisher;

    private readonly BinarySerializer _serializer;
    
    public IpcPublisher(int targetProcessId)
    {
        var queueFactory = new QueueFactory();
        var options = new QueueOptions(queueName: "ghelper-ipc-" + targetProcessId, bytesCapacity: 1024);
        
        _publisher = queueFactory.CreatePublisher(options);
        
        _serializer = new BinarySerializer(1024);
    }
    
    public bool Publish<T>(T message) where T : class, IIpcMessage
    {
        _serializer.Reset();
        _serializer.WriteInt(Environment.ProcessId);
        
        var id = _resolver.GetId<T>();
        
        _serializer.WriteLong(id);
        
        message.Serialize(_serializer);
        
        Log.Information("Publishing message {Message} to process {ProcessId}", typeof(T), Environment.ProcessId);
        
        return _publisher.TryEnqueue(_serializer.AsSpan());
    }
}