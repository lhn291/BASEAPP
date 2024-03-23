using System.Windows;
using System;

namespace BASEAPP.UI.Extensions
{
    public static class EventHandlerHelper
    {
        public static void RegisterEvent(this object sender, RoutedEvent type, RoutedEventHandler handler)
        {
            try
            {
                Application.Current.MainWindow.AddHandler(type, handler);
            }
            catch 
            {
                throw new Exception();
            }
        }

        public static void UnRegisterEvent(this object sender, RoutedEvent type, RoutedEventHandler handler)
        {
            try
            {
                Application.Current.MainWindow.RemoveHandler(type, handler);
            }
            catch
            {
                throw new Exception();
            }
        }
    }

}
