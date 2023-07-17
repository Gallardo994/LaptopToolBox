using System;
using System.Collections.Generic;
using LaptopToolBox.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using Ninject;

namespace LaptopToolBox.AppWindows
{
    public sealed partial class MainWindow
    {
        private readonly Stack<PageStackEntry> _pageBackStack = new();

        [Inject]
        public MainWindow(IPageProvider pageProvider)
        {
            InitializeComponent();
            
            WindowHelper.SetIcon(this, "Assets/appicon.ico");

            NavigationView.DataContext = pageProvider;
            NavigationView.SelectedItem = pageProvider.GetPageItem<Pages.HomePage>();
            
            NavigationView.BackRequested += NavigationViewOnBackRequested;

            AppTitleBar.Loaded += (sender, args) =>
            {
                ExtendsContentIntoTitleBar = true;
                SetTitleBar(AppTitleBar);
                NavigationView.IsTitleBarAutoPaddingEnabled = false;
            };
        }

        private async void NavigationViewOnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (ContentFrame.Content is not IPageBackHandler pageBackHandler)
            {
                TryNavigateBack();
                return;
            }
            
            var result = await pageBackHandler.TryHandleBackAsync();

            if (result)
            {
                TryNavigateBack();
            }
        }

        public void NavigateContentFrame(
            Type pageType, 
            object parameter = null, 
            NavigationTransitionInfo transitionInfo = null,
            bool clearBackStack = true)
        {
            if (clearBackStack)
            {
                _pageBackStack.Clear();
                RefreshBackButtonState();
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
            
            _pageBackStack.Push(new PageStackEntry(currentPageType, parameter, transitionInfo));
            
            RefreshBackButtonState();
        }

        public bool TryNavigateBack()
        {
            if (_pageBackStack.Count == 0)
            {
                return false;
            }

            var item = _pageBackStack.Pop();
            
            NavigateContentFrame(item.SourcePageType, null, item.NavigationTransitionInfo, false);
            RefreshBackButtonState();
            
            return true;
        }
        
        private void RefreshBackButtonState()
        {
            NavigationView.IsBackEnabled = _pageBackStack.Count > 0;
        }

        private void NavigationView_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.SelectedItem is not FlyoutPageItem pageItem)
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
