using System;
using System.Threading;
using System.Threading.Tasks;
using HidLibrary;
using Serilog;

namespace GHelper.Inputs;

public class KeyboardListener : IKeyboardListener
{
    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public KeyboardListener(Action<int> KeyHandler)
    {
        HidDevice? input = AsusUSB.GetDevice();
        if (input == null) return;

        Log.Debug($"Input: {input.DevicePath}");

        var task = Task.Run(() =>
        {
            try
            {
                while (!cancellationTokenSource.Token.IsCancellationRequested)
                {
                    var data = input.Read().Data;
                    if (data.Length > 1 && data[0] == AsusUSB.INPUT_HID_ID && data[1] > 0)
                    {
                        Log.Debug($"Key: {data[1]}");
                        KeyHandler(data[1]);
                    }
                }
                Log.Debug("Listener stopped");

            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }
        });


    }

    public void Dispose()
    {
        cancellationTokenSource?.Cancel();
    }
}