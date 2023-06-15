using GHelper.Injection;
using GHelper.ViewModels;
using Ninject;

namespace GHelper.Views;

public partial class PendingUpdatesWidget
{
    private readonly IStartUpPage _startUpPage;
    private readonly IUpdatesViewModel _updatesViewModel;
    
    public PendingUpdatesWidget() : this(
        Services.ResolutionRoot.Get<IStartUpPage>(),
        Services.ResolutionRoot.Get<IUpdatesViewModel>())
    {
        
    }
    
    [Inject]
    public PendingUpdatesWidget(IStartUpPage startUpPage, IUpdatesViewModel updatesViewModel)
    {
        _startUpPage = startUpPage;
        _updatesViewModel = updatesViewModel;
        
        InitializeComponent();

        BindingContext = _updatesViewModel;
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        _startUpPage.Navigate<UpdatesPage>();
    }
}