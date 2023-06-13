using System.Windows;
using System.Windows.Controls;
using Ninject;
using Wpf.Ui.Controls;

namespace GHelper.Updates.UI;

public class UpdatesWindow : IUpdatesWindow
{
    //private readonly UpdatesWindowDesign _window;
    
    private readonly IUpdatesChecker _updatesChecker;
    private readonly IUpdatesScheduler _updatesScheduler;
    private readonly IModelInfoProvider _modelInfoProvider;
        
    [Inject]
    public UpdatesWindow(IUpdatesChecker updatesChecker,
        IUpdatesScheduler updatesScheduler,
        IModelInfoProvider modelInfoProvider)
    {
        //_window = new UpdatesWindowDesign();
        
        _updatesChecker = updatesChecker;
        _updatesScheduler = updatesScheduler;
        _modelInfoProvider = modelInfoProvider;
    }
    
    public void SetState(bool state)
    {
        if (state)
        {
            //_window.Show();
        }
        else
        {
            //_window.Hide();
        }
    }
}