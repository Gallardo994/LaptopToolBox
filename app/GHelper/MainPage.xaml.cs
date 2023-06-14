using GHelper.Home.ViewModels;
using Ninject;

namespace GHelper;

public partial class MainPage
{
    [Inject]
    public MainPage(IMainPageViewModel mainPageViewModel)
    {
        InitializeComponent();
        
        BindingContext = mainPageViewModel;
    }
}