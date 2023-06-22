using System;
using System.Timers;
using Windows.Graphics;
using GHelper.DeviceControls;
using Microsoft.UI;
using Microsoft.UI.Windowing;

namespace GHelper
{
    public sealed partial class ToastWindow
    {
        private IntPtr _handle;
        private WindowId _windowId;
        private AppWindow _appWindow;
        private OverlappedPresenter _presenter;
        
        private Timer _timer;
        
        private DisplayArea DisplayArea => DisplayArea.GetFromWindowId(_windowId, DisplayAreaFallback.Nearest);

        private byte _alpha;

        public byte Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                Native.SetLayeredWindowAttributes(_handle, 0, _alpha, 0x2);
            }
        }
        private byte MaxAlpha => 255;
        private byte MinAlpha => 0;
        
        public ToastWindow()
        {
            InitializeComponent();
            ModifyWindowAppearance();
            
            _appWindow.Resize(new SizeInt32(512, 128));
            HideOffScreen();
        }

        public void ShowMessage(string title, string message)
        {
            KillTimers();
            
            TitleBlock.Text = title;
            DescriptionBlock.Text = message;
            
            BringToScreen();
            FadeIn();
            StartFadeOutTimer(2000);
        }

        private void BringToScreen()
        {
            var newPosition = _appWindow.Position;
            
            // Center horizontally
            newPosition.X = DisplayArea.WorkArea.Width - _appWindow.Size.Width - 8;
            
            // Show at the lower part of the screen
            newPosition.Y = DisplayArea.WorkArea.Height - _appWindow.Size.Height - 8;
            
            _appWindow.Move(newPosition);
        }
        
        public void HideOffScreen()
        {
            KillTimers();
            
            var newPosition = _appWindow.Position;
            
            // Center horizontally
            newPosition.X = DisplayArea.WorkArea.Width + 200;
            
            // Hide off screen
            newPosition.Y = DisplayArea.WorkArea.Height + 200;
            
            _appWindow.Move(newPosition);

            Alpha = 0;
        }

        private void FadeIn()
        {
            KillTimers();
            
            Alpha = MaxAlpha;
        }
        
        private void StartFadeOutTimer(int interval)
        {
            KillTimers();
            
            _timer = new Timer(interval);
            _timer.Elapsed += (sender, args) => { FadeOut(); };
            _timer.Start();
        }

        private void FadeOut()
        {
            KillTimers();
            
            _timer = new Timer(10);
            _timer.Elapsed += (sender, args) =>
            {
                if (Alpha <= MinAlpha)
                {
                    _timer?.Dispose();
                    _timer = null;
                    
                    HideOffScreen();
                }
                else
                {
                    Alpha -= 5;
                }
            };
            _timer.Start();
        }

        public void KillTimers()
        {
            _timer?.Dispose();
            _timer = null;
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
