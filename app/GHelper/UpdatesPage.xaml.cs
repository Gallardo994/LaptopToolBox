using GHelper.Updates;
using Ninject;

namespace GHelper;

public partial class UpdatesPage
{
    private readonly IUpdatesChecker _updatesChecker;
    public UpdatesViewModel ViewModel { get; init; }
    
    [Inject]
    public UpdatesPage(IUpdatesChecker updatesChecker)
    {
        _updatesChecker = updatesChecker;
        
        InitializeComponent();

        ViewModel = new UpdatesViewModel();
        
        BindingContext = ViewModel;
        
        RefreshUpdates();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        RefreshUpdates();
    }
    
    private void DownloadButton_OnClicked(object? sender, EventArgs e)
    {
        var button = sender as Button;
        var update = button?.BindingContext as IUpdate;
        
        if (update == null)
        {
            return;
        }

        Launcher.OpenAsync(update.DownloadUrl);
    }

    private void RefreshUpdates()
    {
        ViewModel.IsUpdating = true;
        Task.Run(async () =>
        {
            var result = await _updatesChecker.CheckForUpdates();
            MainThread.BeginInvokeOnMainThread(() =>
            {
                ViewModel.SetUpdates(result);
                ViewModel.IsUpdating = false;
            });
        }).Forget();
    }
}