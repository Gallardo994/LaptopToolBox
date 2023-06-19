using System;
using System.Collections.Generic;
using Ninject;
using Serilog;

namespace GHelper.DeviceControls.Keyboard.Vendors.Asus;

public class AsusVendorKeyboardHandler : IVendorKeyboardHandler
{
    private readonly IVendorKeyboardListener _vendorKeyboardListener;
    private readonly Dictionary<int, List<IVendorKeyBind>> _keyHandlers;

    [Inject]
    public AsusVendorKeyboardHandler(IVendorKeyboardListener vendorKeyboardListener)
    {
        _vendorKeyboardListener = vendorKeyboardListener;
        _keyHandlers = new Dictionary<int, List<IVendorKeyBind>>();
        
        _vendorKeyboardListener.KeyHandler += KeyHandler;
    }

    private void KeyHandler(int keyCode)
    {
        if (!_keyHandlers.ContainsKey(keyCode))
        {
            Log.Debug("No key handlers for key {KeyCode}", keyCode);
            return;
        }
        
        foreach (var handler in _keyHandlers[keyCode])
        {
            try
            {
                handler.Execute();
            }
            catch (Exception ex)
            {
                Log.Debug(ex.ToString());
            }
        }
        
        Log.Debug("Key {KeyCode} pressed", keyCode);
    }
    
    public void Bind(IVendorKeyBind keyBind)
    {
        if (!_keyHandlers.ContainsKey(keyBind.Key))
        {
            _keyHandlers[keyBind.Key] = new List<IVendorKeyBind>();
        }
        
        _keyHandlers[keyBind.Key].Add(keyBind);
    }
    
    public void Unbind(IVendorKeyBind keyBind)
    {
        if (!_keyHandlers.ContainsKey(keyBind.Key))
        {
            Log.Debug("No key handlers for key {KeyCode}", keyBind.Key);
            return;
        }
        
        _keyHandlers[keyBind.Key].Remove(keyBind);
        
        if (_keyHandlers[keyBind.Key].Count == 0)
        {
            _keyHandlers.Remove(keyBind.Key);
        }
    }
    
    public void Dispose()
    {
        _vendorKeyboardListener.KeyHandler -= KeyHandler;
    }
}