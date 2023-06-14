using GHelper.Updates;
using Ninject;

namespace GHelper;

public partial class UpdatesPage
{
    private readonly IUpdatesChecker _updatesChecker;
    public IUpdatesViewModel ViewModel { get; }

    [Inject]
    public UpdatesPage(IUpdatesViewModel viewModel,
        IUpdatesChecker updatesChecker)
    {
        ViewModel = viewModel;
        _updatesChecker = updatesChecker;
        
        InitializeComponent();
        
        BindingContext = ViewModel;
        
        RefreshUpdates();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        RefreshUpdates();
    }
    
    private async void DownloadButton_OnClicked(object? sender, EventArgs e)
    {
        var button = sender as Button;
        var update = button?.BindingContext as IUpdate;
        
        if (update == null)
        {
            return;
        }
        
        var result = await DisplayAlert("Download", $"Go to {update.DownloadUrl}", "Yes", "No");
        if (!result)
        {
            return;
        }

        await Launcher.OpenAsync(update.DownloadUrl);
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