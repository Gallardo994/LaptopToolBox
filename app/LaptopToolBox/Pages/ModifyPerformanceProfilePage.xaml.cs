using System;
using System.Threading.Tasks;
using LaptopToolBox.AppWindows;
using LaptopToolBox.DeviceControls.Fans;
using LaptopToolBox.DeviceControls.PerformanceModes;
using LaptopToolBox.Injection;
using LaptopToolBox.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Navigation;
using Ninject;
using Ninject.Parameters;
using Serilog;

namespace LaptopToolBox.Pages
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

        private void CpuPresetsFlyout_Loaded(object sender, RoutedEventArgs e)
        {
            CpuUpdateFlyoutItems();
            ViewModel.FanController.IntegratedCpuFanCurves.CollectionChanged += IntegratedCpuFanCurves_CollectionChanged;
        }
        
        private void IntegratedCpuFanCurves_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CpuUpdateFlyoutItems();
        }
        
        private void CpuUpdateFlyoutItems()
        {
            CpuPresetsFlyout.Items.Clear();

            var index = 0;
            
            foreach (var curve in ViewModel.FanController.IntegratedCpuFanCurves)
            {
                var name = "CPU Fan Preset #" + ++index;
                var menuItem = new MenuFlyoutItem
                {
                    Text = name,
                };
                menuItem.Click += (s, e) => CpuFanCurvePresetClicked(curve, e);
                CpuPresetsFlyout.Items.Add(menuItem);
            }
        }
        
        private void CpuFanCurvePresetClicked(object sender, RoutedEventArgs e)
        {
            var fanCurve = (FanCurve) sender;
            
            Log.Debug("Applying preset fan curve {@FanCurve}", fanCurve);

            fanCurve.CopyTo(ViewModel.Modified.CpuFanCurve);
        }
        
        private void GpuPresetsFlyout_Loaded(object sender, RoutedEventArgs e)
        {
            GpuUpdateFlyoutItems();
            ViewModel.FanController.IntegratedCpuFanCurves.CollectionChanged += IntegratedGpuFanCurves_CollectionChanged;
        }
        
        private void IntegratedGpuFanCurves_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            GpuUpdateFlyoutItems();
        }
        
        private void GpuUpdateFlyoutItems()
        {
            GpuPresetsFlyout.Items.Clear();

            var index = 0;
            
            foreach (var curve in ViewModel.FanController.IntegratedGpuFanCurves)
            {
                var name = "GPU Fan Preset #" + ++index;
                var menuItem = new MenuFlyoutItem
                {
                    Text = name,
                };
                menuItem.Click += (s, e) => GpuFanCurvePresetClicked(curve, e);
                GpuPresetsFlyout.Items.Add(menuItem);
            }
        }
        
        private void GpuFanCurvePresetClicked(object sender, RoutedEventArgs e)
        {
            var fanCurve = (FanCurve) sender;
            
            fanCurve.CopyTo(ViewModel.Modified.GpuFanCurve);
        }
    }
}
