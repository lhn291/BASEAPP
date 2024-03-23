using BASEAPP.UI.Extensions;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows;
using System.Windows.Input;

namespace BASEAPP.UI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private IRegionManager regionManager;

        public MainWindowViewModel(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            regionManager.RequestNavigate(nameof(Shopping));
            this.RegisterEvent(UIElement.PreviewKeyDownEvent, new RoutedEventHandler(OnKeyDown));
        }
        private void OnKeyDown(object sender, RoutedEventArgs ev)
        {
            var keyEv = ev as KeyEventArgs;
            if (keyEv != null && keyEv.Key == Key.Escape)
            {
                System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
                currentProcess?.Kill();
            }
        }

    }
}
