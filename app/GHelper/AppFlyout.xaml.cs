using Ninject;
using Ninject.Syntax;

namespace GHelper;

public partial class AppFlyout : IStartUpPage
{
    private readonly IResolutionRoot _resolutionRoot;
    private readonly IPageProvider _pageProvider;
    
    [Inject]
    public AppFlyout(IResolutionRoot resolutionRoot, IPageProvider pageProvider)
    {
        _resolutionRoot = resolutionRoot;
        _pageProvider = pageProvider;
        
        InitializeComponent();
        
        flyoutPage.BindingContext = _pageProvider;
        flyoutPage.FlyoutItems.SelectionChanged += OnSelectionChanged;
        
        Loaded += OnLoaded;
    }
    
    private void OnLoaded(object? sender, EventArgs eventArgs)
    {
        var pageItem = _pageProvider.GetHomePageItem();
        flyoutPage.FlyoutItems.SelectedItem = pageItem;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        
        if (item == null)
        {
            return;
        }

        var pageItem = _pageProvider.GetPageItem(item.TargetType);

        if (pageItem == null)
        {
            return;
        }

        var resolvedPage = _resolutionRoot.Get(item.TargetType);

        if (resolvedPage is not Page page)
        {
            return;
        }

        var navigationPage = new NavigationPage(page);
        Detail = navigationPage;
    }
    
    public void Navigate<T>() where T : Page
    {
        Navigate(typeof(T));
    }

    public void Navigate(Type type)
    {
        var pageItem = _pageProvider.GetPageItem(type);
        flyoutPage.FlyoutItems.SelectedItem = pageItem;
    }
}