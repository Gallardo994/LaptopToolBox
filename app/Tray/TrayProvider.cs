using GHelper.Settings;
using Ninject;

namespace GHelper.Tray;

public class TrayProvider : ITrayProvider
{
    private readonly ISettingsFormController _settingsFormController;
    
    private readonly NotifyIcon _trayIcon;
    
    [Inject]
    public TrayProvider(ISettingsFormController settingsFormController)
    {
        _settingsFormController = settingsFormController;
        
        _trayIcon = new NotifyIcon
        {
            Text = "G-Helper",
            Icon = Properties.Resources.standard,
            Visible = true,
        };
        
        _trayIcon.MouseClick += MouseClickHandler;
        _trayIcon.MouseMove += MouseMoveHandler;
    }
    
    private void MouseClickHandler(object? sender, MouseEventArgs e)
    {
        if (e.Button != MouseButtons.Left)
        {
            return;
        }
        
        _settingsFormController.Toggle();
    }
    
    private void MouseMoveHandler(object? sender, MouseEventArgs e)
    {
        Program._settingsForm.RefreshSensors();
    }
    
    public void SetVisible(bool visible)
    {
        _trayIcon.Visible = visible;
    }

    public void SetToolTip(string text)
    {
        _trayIcon.Text = text;
    }

    public void SetIcon(Icon? icon)
    {
        _trayIcon.Icon = icon;
    }
    
    public void SetContextMenuStrip(ContextMenuStrip? contextMenuStrip)
    {
        _trayIcon.ContextMenuStrip = contextMenuStrip;
    }

    public void Dispose()
    {
        _trayIcon.MouseClick -= MouseClickHandler;
        _trayIcon.MouseMove -= MouseMoveHandler;
        _trayIcon.Dispose();
    }
}