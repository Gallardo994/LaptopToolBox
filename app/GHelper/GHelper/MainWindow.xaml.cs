using System;
using GHelper.Backdrops;
using Microsoft.UI.Xaml.Controls;
using Ninject;

namespace GHelper
{
    public sealed partial class MainWindow
    {
        [Inject]
        public MainWindow(IBackdropProvider backdropProvider, IPageProvider pageProvider)
        {
            InitializeComponent();

            if (backdropProvider.TryGetCompatibleBackdrop(out var backdrop))
            {
                SystemBackdrop = backdrop;
            }

            NavigationView.DataContext = pageProvider;
            NavigationView.SelectedItem = pageProvider.GetPageItem<Pages.HomePage>();
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

            ContentFrame.Navigate(pageItem.TargetType);
        }
    }
}
