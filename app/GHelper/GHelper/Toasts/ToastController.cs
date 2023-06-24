using System.Timers;
using Windows.Graphics;
using GHelper.AppWindows;
using GHelper.Helpers;
using GHelper.Helpers.Native;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Ninject;

namespace GHelper.Toasts;

public class ToastController : IToastController
{
    private readonly ToastWindow _toastWindow;
    
    private Timer _timer;
    private DisplayArea DisplayArea => WindowHelper.GetDisplayArea(_toastWindow);

    private const int FadeOutStartInterval = 2000;
    private const int FadeOutInterval = 10;
    private const byte FadeOutStep = 5;
    private const byte MaxAlpha = 255;
    private const byte MinAlpha = 0;

    [Inject]
    public ToastController(ToastWindow toastWindow)
    {
        _toastWindow = toastWindow;
        
        WindowHelper.ConvertToOverlay(_toastWindow);
        WindowHelper.SetRoundedCorners(_toastWindow, DwmWindowCornerPreference.Round);
        WindowHelper.SetTransparent(_toastWindow, true);

        var appWindow = WindowHelper.GetAppWindowOf(_toastWindow);
        var size = new SizeInt32(512, 128);
        var position = new PointInt32(
            DisplayArea.WorkArea.Width - size.Width - 8, 
            DisplayArea.WorkArea.Height - size.Height - 8);
            
        appWindow.Resize(size);
        appWindow.Move(position);
            
        _toastWindow.Hide();
    }
    
    public void ShowToast(string glyphKey, string title, string message)
    {
        KillTimers();

        _toastWindow.GlyphIcon.Glyph = glyphKey;
        _toastWindow.TitleBlock.Text = title;
        
        _toastWindow.DescriptionBlock.Text = message;
        _toastWindow.DescriptionBlock.Visibility = string.IsNullOrEmpty(message) ? Visibility.Collapsed : Visibility.Visible;
            
        _toastWindow.Show();
            
        _toastWindow.Alpha = MaxAlpha;
        StartFadeOutTimer(FadeOutStartInterval);
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
            
        _timer = new Timer(FadeOutInterval);
        _timer.Elapsed += (sender, args) =>
        {
            if (_toastWindow.Alpha <= MinAlpha)
            {
                _timer?.Dispose();
                _timer = null;
                    
                _toastWindow.Hide();
            }
            else
            {
                _toastWindow.Alpha -= FadeOutStep;
            }
        };
        _timer.Start();
    }

    private void KillTimers()
    {
        _timer?.Dispose();
        _timer = null;
    }
}