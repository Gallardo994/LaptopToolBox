using System.Timers;
using Windows.Graphics;
using GHelper.Helpers;
using GHelper.Helpers.Native;
using Microsoft.UI.Windowing;

namespace GHelper.AppWindows
{
    public sealed partial class ToastWindow
    {
        private Timer _timer;

        private DisplayArea DisplayArea => WindowHelper.GetDisplayArea(this);

        private byte _alpha;

        public byte Alpha
        {
            get => _alpha;
            set
            {
                _alpha = value;
                WindowHelper.SetWindowAlpha(this, _alpha);
            }
        }
        private byte MaxAlpha => 255;
        private byte MinAlpha => 0;
        
        public ToastWindow()
        {
            InitializeComponent();
            
            WindowHelper.ConvertToOverlay(this);
            WindowHelper.SetRoundedCorners(this, DwmWindowCornerPreference.Round);
            WindowHelper.SetTransparent(this, true);

            var appWindow = WindowHelper.GetAppWindowOf(this);
            appWindow.Resize(new SizeInt32(512, 128));

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
            var appWindow = WindowHelper.GetAppWindowOf(this);
            
            var newPosition = new PointInt32(
                DisplayArea.WorkArea.Width - appWindow.Size.Width - 8, 
                DisplayArea.WorkArea.Height - appWindow.Size.Height - 8);
            
            appWindow.Move(newPosition);
        }
        
        public void HideOffScreen()
        {
            KillTimers();
            
            var appWindow = WindowHelper.GetAppWindowOf(this);
            var newPosition = new PointInt32(
                DisplayArea.WorkArea.Width + 200, 
                DisplayArea.WorkArea.Height + 200);

            appWindow.Move(newPosition);

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
    }
}
