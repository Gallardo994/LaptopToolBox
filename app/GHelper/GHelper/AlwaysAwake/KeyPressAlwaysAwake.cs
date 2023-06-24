using System.Runtime.InteropServices;
using System.Threading;
using Vanara.PInvoke;

namespace GHelper.AlwaysAwake;

public class KeyPressAlwaysAwake : IAlwaysAwakeController
{
    private readonly int _keyPressInterval;
    private readonly int _keyCode;
    
    private Thread _thread;
    
    public KeyPressAlwaysAwake()
    {
        _keyPressInterval = 5000;
        _keyCode = 0x7E; // F15
    }
    
    public void Start()
    {
        Stop();
        
        _thread = new Thread(ThreadRunner);
        _thread.Start();
    }
    
    public void Stop()
    {
        _thread?.Interrupt();
        _thread?.Join();
    }
    
    public bool IsRunning => _thread != null && _thread.IsAlive;
    
    private void ThreadRunner()
    {
        try
        {
            while (true)
            {
                User32.keybd_event((byte) _keyCode, 0, 0, 0);
                Thread.Sleep(_keyPressInterval);
            }
        }
        catch (ThreadInterruptedException)
        {
            
        }
    }
}