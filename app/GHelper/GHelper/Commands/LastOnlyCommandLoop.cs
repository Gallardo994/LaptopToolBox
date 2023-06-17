using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using Serilog;

namespace GHelper.Commands
{
    public abstract class LastOnlyCommandLoop<T> where T : ICommand
    {
        private readonly ConcurrentQueue<T> _commands;
        private readonly CancellationTokenSource _cancellationTokenSource;
        
        private readonly object _syncRoot;
        
        private readonly Thread _thread;

        public LastOnlyCommandLoop()
        {
            _commands = new ConcurrentQueue<T>();
            _cancellationTokenSource = new CancellationTokenSource();
            
            _syncRoot = new object();
            
            _thread = new Thread(() => Run(_cancellationTokenSource.Token));
            _thread.Start();
        }

        public void Enqueue(T command)
        {
            _commands.Enqueue(command);
            lock (_syncRoot)
            {
                Monitor.Pulse(_syncRoot);
            }
        }

        private void Run(CancellationToken cancellationToken)
        {
            while (true)
            {
                try
                {
                    ExecuteCommands();

                    lock (_syncRoot)
                    {
                        while (!_commands.Any() && !cancellationToken.IsCancellationRequested)
                        {
                            Monitor.Wait(_syncRoot);
                        }
                    }

                    cancellationToken.ThrowIfCancellationRequested();
                }
                catch (OperationCanceledException)
                {
                    Log.Information("Stopping Command Loop");
                    break;
                }
            }
        }

        private void ExecuteCommands()
        {
            var lastCommand = default(T);

            while (_commands.TryDequeue(out var command))
            {
                lastCommand = command;
            }
            
            if (lastCommand == null)
            {
                return;
            }
            
            try
            {
                lastCommand.Execute();
            } 
            catch (Exception e)
            {
                Log.Error(e, "Error executing command");
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            
            lock (_syncRoot)
            {
                Monitor.Pulse(_syncRoot);
            }
            
            _thread.Join();

            _cancellationTokenSource.Dispose();
        }
    }
}
