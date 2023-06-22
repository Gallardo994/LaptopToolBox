using GHelper.AppWindows;
using Ninject;

namespace GHelper.Initializers.ConcreteInitializers;

public class MainWindowInitializer : IInitializer
{
    private readonly MainWindow _mainWindow;
    
    [Inject]
    public MainWindowInitializer(MainWindow mainWindow)
    {
        _mainWindow = mainWindow;
    }
    
    public void Initialize()
    {
        _mainWindow.Activate();
        
        /*
        _mainWindow.Closed += (sender, windowArgs) =>
        {
            windowArgs.Handled = true;
        };
        */
    }
}