using System.Collections.Concurrent;
using Serilog;

namespace GHelper.Commands
{
    public class CommandLoop : ICommandLoop
    {
        private readonly ConcurrentQueue<ICommand> _commands;
        private readonly CancellationTokenSource _cancellationTokenSource;
        
        private readonly object _syncRoot;
        
        private readonly Thread _thread;

        public CommandLoop()
        {
            _commands = new ConcurrentQueue<ICommand>();
            _cancellationTokenSource = new CancellationTokenSource();
            
            _syncRoot = new object();
            
            _thread = new Thread(() => Run(_cancellationTokenSource.Token));
            _thread.Start();
        }

        public void Add(ICommand command)
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
            while (_commands.TryDequeue(out var command))
            {
                try
                {
                    command.Execute();
                } 
                catch (Exception e)
                {
                    Log.Error(e, "Error executing command");
                }
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
