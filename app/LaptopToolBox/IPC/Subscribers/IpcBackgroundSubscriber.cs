using System;
using System.Collections.Generic;
using System.Timers;
using Cloudtoid.Interprocess;
using LaptopToolBox.Commands;
using LaptopToolBox.Helpers;
using LaptopToolBox.Serialization;
using Ninject;
using Serilog;

namespace LaptopToolBox.IPC.Subscribers;

public class IpcBackgroundSubscriber : IIpcBackgroundSubscriber
{
    private readonly IIpcResolver _resolver;
    private readonly ISTACommandLoop _staCommandLoop;
    private readonly ISubscriber _subscriber;
    
    private readonly Dictionary<long, Action<int, IIpcMessage>> _callbacks;
    
    private readonly SafeTimer _timer;

    private readonly byte[] _buffer = new byte[1024];
    private readonly BinaryDeserializer _deserializer;
    
    [Inject]
    public IpcBackgroundSubscriber(IIpcResolver ipcResolver, ISTACommandLoop staCommandLoop)
    {
        _resolver = ipcResolver;
        _staCommandLoop = staCommandLoop;
        
        var queueFactory = new QueueFactory();
        var queueName = "ltb-ipc-" + Environment.ProcessId;
        var options = new QueueOptions(queueName: queueName, bytesCapacity: _buffer.Length);
        _subscriber = queueFactory.CreateSubscriber(options);
        
        _callbacks = new Dictionary<long, Action<int, IIpcMessage>>();
        
        _timer = new SafeTimer(10);
        _timer.Elapsed += OnTimerElapsed;
        
        _deserializer = new BinaryDeserializer(_buffer);
        
        Log.Information("Created IPC subscriber for process {ProcessId}", Environment.ProcessId);
    }
    
    public void StartListening()
    {
        Log.Information("Starting to listen for IPC messages");
        _timer.Start();
    }
    
    public void StopListening()
    {
        Log.Information("Stopping listening for IPC messages");
        _timer.Stop();
    }
    
    public void Subscribe<T>(Action<int, T> callback) where T : class, IIpcMessage
    {
        var id = _resolver.GetId<T>();
        
        Log.Information("Subscribing to message {Message}", typeof(T));
        
        if (!_callbacks.ContainsKey(id))
        {
            _callbacks.Add(id, (processId, message) => callback(processId, (T) message));
        }
        else
        {
            _callbacks[id] += (processId, message) => callback(processId, (T) message);
        }
    }
    
    private void OnTimerElapsed(object sender, ElapsedEventArgs e)
    {
        while (_subscriber.TryDequeue(_buffer, default, out _))
        {
            _deserializer.ResetPosition();

            var processId = _deserializer.ReadInt();
            Log.Information("Received message from process {ProcessId}", processId);
            
            var id = _deserializer.ReadLong();
            
            if (!_resolver.TryGetType(id, out var type))
            {
                continue;
            }
            
            Log.Information("Received message {Message} from process {ProcessId}", type, processId);
            
            var message = (IIpcMessage) Activator.CreateInstance(type);
            if (message == null)
            {
                continue;
            }
            
            message.Deserialize(_deserializer);
            
            _staCommandLoop.Enqueue(() =>
            {
                _callbacks[id](processId, message);
            });
        }
    }
}