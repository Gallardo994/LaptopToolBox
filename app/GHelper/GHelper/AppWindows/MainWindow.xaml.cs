using System;
using GHelper.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Ninject;
using Serilog;

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
        
        public void NavigateContentFrame(
            Type pageType, 
            object parameter = null, 
            NavigationTransitionInfo transitionInfo = null,
            bool clearBackStack = true)
        {
            if (clearBackStack)
            {
                ContentFrame.BackStack.Clear();
            }
            
            ContentFrame.Navigate(pageType, parameter, transitionInfo);
        }
        
        public void AddThisPageToBackStack(object parameter = null, 
            NavigationTransitionInfo transitionInfo = null)
        {
            var currentPageType = ContentFrame.Content?.GetType();
            
            if (currentPageType == null)
            {
                throw new InvalidOperationException("Cannot add a page to the back stack when there is no page currently displayed");
            }
            
            ContentFrame.BackStack.Add(new PageStackEntry(currentPageType, parameter, transitionInfo));
        }

        public bool TryNavigateBack()
        {
            if (!ContentFrame.CanGoBack)
            {
                return false;
            }

            ContentFrame.GoBack();
            return true;
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

            NavigateContentFrame(pageItem.TargetType, null, new DrillInNavigationTransitionInfo());
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
