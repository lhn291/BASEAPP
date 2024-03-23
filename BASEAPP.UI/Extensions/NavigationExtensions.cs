using BASEAPP.UI.Enums;
using Prism.Regions;
using System;
using System.Windows;

namespace BASEAPP.UI.Extensions
{
    internal static class NavigationExtensions
    {
        public static void RequestNavigatePopup(this IRegionManager regionManager, string viewName, NavigationParameters param = null)
        {
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
            {
                regionManager.RequestNavigate(RegionEnum.PopupRegion.ToString(), viewName, param ?? new NavigationParameters());
            }));
        }

        public static void RequestNavigate(this IRegionManager regionManager, string viewName, NavigationParameters param = null)
        {
            Application.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded, new Action(() =>
            {
                regionManager.RequestNavigate(RegionEnum.ContentRegion.ToString(), viewName, param ?? new NavigationParameters());
            }));
        }
    }
}
