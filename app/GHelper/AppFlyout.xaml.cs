using Ninject;
using Ninject.Syntax;

namespace GHelper;

public partial class AppFlyout : IStartUpPage
{
    private readonly IResolutionRoot _resolutionRoot;
    
    [Inject]
    public AppFlyout(IResolutionRoot resolutionRoot)
    {
        _resolutionRoot = resolutionRoot;
        InitializeComponent();
        
        flyoutPage.FlyoutItems.SelectionChanged += OnSelectionChanged;
    }

    private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        var item = e.CurrentSelection.FirstOrDefault() as FlyoutPageItem;
        
        if (item == null)
        {
            return;
        }

        var resolvedPage = _resolutionRoot.Get(item.TargetType);
        
        if (resolvedPage is not Page page)
        {
            return;
        }

        Detail = new NavigationPage(page);
    }
}