﻿using System;
using System.Threading;
using HidLibrary;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.Keyboard;

public class AsusKeyboardListener : IVendorKeyboardListener
{
    public Action<int> KeyHandler { get; set; }
    
    private readonly IUsb _usb;
    private readonly IHid _hid;
    
    private readonly CancellationTokenSource _cts;
    private readonly Thread _thread;
    
    [Inject]
    public AsusKeyboardListener(IUsb usb, IHid hid)
    {
        _usb = usb;
        _hid = hid;
        
        _cts = new CancellationTokenSource();
        _thread = new Thread(ThreadHandler);
        
        _thread.Start();
    }

    private void ThreadHandler()
    {
        var input = _hid.GetDevice(_usb.VendorId, _usb.DeviceIds, _usb.InputHidId);
        
        if (input == null)
        {
            Log.Debug("No input device found");
            return;
        }
        
        while (!_cts.Token.IsCancellationRequested)
        {
            try
            {
                var data = input.Read().Data;
                if (data?.Length > 1 && data[0] == _usb.InputHidId && data[1] > 0)
                {
                    KeyHandler?.Invoke(data[1]);
                }
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }

            Thread.Sleep(100);
        }
    }

    public void Dispose()
    {
        _cts?.Cancel();
        _thread?.Join();
    }
}