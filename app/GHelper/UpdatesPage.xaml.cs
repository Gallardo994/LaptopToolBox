using GHelper.Updates;

namespace GHelper;

public partial class UpdatesPage
{
    public UpdatesViewModel ViewModel { get; init; }
    
    public UpdatesPage()
    {
        InitializeComponent();

        ViewModel = new UpdatesViewModel();
        
        BindingContext = ViewModel;
    }
}