using GHelper.AppWindows;
using GHelper.Helpers;
using Ninject;

namespace GHelper.Controllers;

public class MainWindowController : IMainWindowController
{
    private readonly MainWindow _mainWindow;
    
    [Inject]
    public MainWindowController(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }

    public void SetState(bool state)
    {
        if (state)
        {
            _mainWindow.Show();
        }
        else
        {
            _mainWindow.Hide();
        }
    }
    
    public bool GetState()
    {
        return _mainWindow.Visible;
    }
    
    public void ToggleState()
    {
        SetState(!GetState());
    }
}