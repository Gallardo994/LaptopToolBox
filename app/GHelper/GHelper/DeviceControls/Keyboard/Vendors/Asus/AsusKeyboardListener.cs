using System;
using System.Threading;
using GHelper.Commands;
using GHelper.DeviceControls.Usb;
using HidLibrary;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus;

public class AsusKeyboardListener : IVendorKeyboardListener
{
    public Action<int> KeyHandler { get; set; }
    
    private readonly IUsb _usb;
    private readonly IHid _hid;
    private readonly ISTACommandLoop _staCommandLoop;
    
    private readonly CancellationTokenSource _cts;
    private readonly Thread _thread;
    
    [Inject]
    public AsusKeyboardListener(IUsb usb, IHid hid, ISTACommandLoop staCommandLoop)
    {
        _usb = usb;
        _hid = hid;
        _staCommandLoop = staCommandLoop;
        
        _cts = new CancellationTokenSource();
        _thread = new Thread(ThreadHandler);
        
        _thread.Start();
    }

    private void ThreadHandler()
    {
        Log.Debug("Starting Asus keyboard listener");
        
        var input = default(HidDevice);

        bool DetectInputDevice()
        {
            input = _hid.GetDevice(_usb.VendorId, _usb.DeviceIds, _usb.InputHidId);

            if (input is not { IsConnected: true })
            {
                return false;
            }
            
            Log.Debug($"Detected new input device: {input.DevicePath}");
            return true;
        }

        while (!_cts.Token.IsCancellationRequested)
        {
            try
            {
                if ((input == null || !input.IsConnected) && !DetectInputDevice())
                {
                    Thread.SpinWait(1000);
                    continue;
                }
                
                var data = input.Read().Data;
                if (data?.Length > 1 && data[0] == _usb.InputHidId && data[1] > 0)
                {
                    _staCommandLoop.Enqueue(() =>
                    {
                        KeyHandler?.Invoke(data[1]);
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }

            Thread.Sleep(10);
        }
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _thread?.Join();
    }
}