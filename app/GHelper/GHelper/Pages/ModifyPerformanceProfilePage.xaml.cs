using System;
using System.Threading.Tasks;
using GHelper.AppWindows;
using GHelper.DeviceControls.PerformanceModes;
using GHelper.Injection;
using GHelper.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using Ninject;
using Ninject.Parameters;
using Serilog;

namespace GHelper.Pages
{
    public sealed partial class ModifyPerformanceProfilePage : IPageBackHandler
    {
        private readonly IPerformanceModesProvider _performanceModesProvider = Services.ResolutionRoot.Get<IPerformanceModesProvider>();
        private readonly MainWindow _mainWindow = Services.ResolutionRoot.Get<MainWindow>();
        
        public ModifyPerformanceProfileViewModel ViewModel { get; set; }
        
        public ModifyPerformanceProfilePage()
        {
            InitializeComponent();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            var guid = (Guid) e.Parameter;
            var mode = _performanceModesProvider.FindById(guid);
            
            ViewModel = Services.ResolutionRoot.Get<ModifyPerformanceProfileViewModel>(
                new ConstructorArgument("performanceMode", mode));
        }

        private async void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            var contentDialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Content = "Do you want to save your changes?",
                PrimaryButtonText = "Save Changes",
                CloseButtonText = "Continue Editing",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonCommandParameter = false,
                PrimaryButtonCommandParameter = true,
            };
            
            var result = await contentDialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                return;
            }
            
            ViewModel.ApplyModificationsFromCustomPerformanceMode();
            _mainWindow.TryNavigateBack();
        }
        
        private async void UndoButton_OnClick(object sender, RoutedEventArgs e)
        {
            var contentDialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Content = "Do you want to discard your changes?",
                PrimaryButtonText = "Discard Changes",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonCommandParameter = false,
                PrimaryButtonCommandParameter = true,
            };
            
            var result = await contentDialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                return;
            }
            
            ViewModel.UndoModifications();
        }

        private async void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!ViewModel.IsDirty())
            {
                _mainWindow.TryNavigateBack();
                return;
            }
            
            var contentDialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Content = "You have unsaved changes. Are you sure you want to discard them?",
                PrimaryButtonText = "Discard Changes",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonCommandParameter = false,
                PrimaryButtonCommandParameter = true,
            };
            
            var result = await contentDialog.ShowAsync();

            if (result != ContentDialogResult.Primary)
            {
                return;
            }
            
            _mainWindow.TryNavigateBack();
        }

        public async Task<bool> TryHandleBackAsync()
        {
            if (!ViewModel.IsDirty())
            {
                return true;
            }
            
            var contentDialog = new ContentDialog
            {
                XamlRoot = XamlRoot,
                Content = "You have unsaved changes. Are you sure you want to discard them?",
                PrimaryButtonText = "Discard Changes",
                CloseButtonText = "Cancel",
                DefaultButton = ContentDialogButton.Primary,
                CloseButtonCommandParameter = false,
                PrimaryButtonCommandParameter = true,
            };
            
            var result = await contentDialog.ShowAsync();
            return result == ContentDialogResult.Primary;
        }

        private void PowerLimitValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            if (SplSlider.Value > FpptSlider.Value)
            {
                FpptSlider.Value = SplSlider.Value;
            }

            if (SplSlider.Value > SpptSlider.Value)
            {
                SpptSlider.Value = SplSlider.Value;
            }
        }
    }
}
