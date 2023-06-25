using System;
using GHelper.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Ninject;

namespace GHelper.AppWindows
{
    public sealed partial class MainWindow
    {
        [Inject]
        public MainWindow(IPageProvider pageProvider)
        {
            InitializeComponent();
            
            WindowHelper.SetIcon(this, "Assets/appicon.ico");

            NavigationView.DataContext = pageProvider;
            NavigationView.SelectedItem = pageProvider.GetPageItem<Pages.HomePage>();
            
            AppTitleBar.Loaded += (sender, args) =>
            {
                ExtendsContentIntoTitleBar = true;
                SetTitleBar(AppTitleBar);
                NavigationView.IsTitleBarAutoPaddingEnabled = false;
            };
        }

        private void NavigationView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            var pageItem = args.SelectedItem as FlyoutPageItem;

            if (pageItem == null)
            {
                throw new InvalidOperationException("The selected item is not a FlyoutPageItem");
            }
            
            if (pageItem.TargetType == null)
            {
                return;
            }

            ContentFrame.Navigate(pageItem.TargetType, null, new DrillInNavigationTransitionInfo());
        }
        
        private void NavigationView_OnPaneDisplayModeChanged(NavigationView sender, NavigationViewDisplayModeChangedEventArgs args)
        {
            if (sender.PaneDisplayMode == NavigationViewPaneDisplayMode.Top)
            {
                VisualStateManager.GoToState(sender, "Top", true);
            }
            else
            {
                if (args.DisplayMode == NavigationViewDisplayMode.Minimal)
                {
                    VisualStateManager.GoToState(sender, "Compact", true);
                }
                else
                {
                    VisualStateManager.GoToState(sender, "Default", true);
                }
            }
        }
    }
}
