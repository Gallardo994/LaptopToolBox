using System;
using Windows.Graphics;
using GHelper.DeviceControls;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Ninject;

namespace GHelper
{
    public sealed partial class ToastWindow
    {
        private IntPtr _handle;
        private WindowId _windowId;
        private AppWindow _appWindow;
        private OverlappedPresenter _presenter;
        
        private DisplayArea DisplayArea => DisplayArea.GetFromWindowId(_windowId, DisplayAreaFallback.Nearest);
        
        public ToastWindow()
        {
            InitializeComponent();

            ModifyWindowAppearance();
            
            _appWindow.Resize(new SizeInt32(400, 100));
            HideOffScreen();
        }

        private void BringToScreen()
        {
            var newPosition = _appWindow.Position;
            
            // Center horizontally
            newPosition.X = (DisplayArea.WorkArea.Width - _appWindow.Size.Width) / 2;
            
            // Show at the lower part of the screen
            newPosition.Y = DisplayArea.WorkArea.Height - _appWindow.Size.Height - 200;
            
            _appWindow.Move(newPosition);
            
            SetAlpha(200);
        }
        
        private void HideOffScreen()
        {
            var newPosition = _appWindow.Position;
            
            // Center horizontally
            newPosition.X = (DisplayArea.WorkArea.Width - _appWindow.Size.Width) / 2;
            
            // Hide off screen
            newPosition.Y = DisplayArea.WorkArea.Height + 200;
            
            _appWindow.Move(newPosition);
            
            SetAlpha(0);
        }

        private void SetAlpha(byte alpha)
        {
            Native.SetLayeredWindowAttributes(_handle, 0, alpha, 0x2);
        }
        
        private void ModifyWindowAppearance()
        {
            _handle = WinRT.Interop.WindowNative.GetWindowHandle(this);
            Native.SetWindowLong(_handle, -20, Native.GetWindowLong(_handle, -20) ^ 0x80000);
            
            _windowId = Win32Interop.GetWindowIdFromWindow(_handle);
            
            _appWindow = AppWindow.GetFromWindowId(_windowId);
            _appWindow.IsShownInSwitchers = false;

            _presenter = _appWindow.Presenter as OverlappedPresenter;
            
            if (_presenter == null)
            {
                throw new InvalidOperationException("Unable to extract presenter from AppWindow");
            }
            
            _presenter.IsMaximizable = false;
            _presenter.IsMinimizable = false;
            _presenter.IsAlwaysOnTop = true;
            _presenter.IsResizable = false;
            
            _presenter.SetBorderAndTitleBar(false, false);
        }
    }
}
