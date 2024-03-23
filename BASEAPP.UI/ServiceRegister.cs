using BASEAPP.UI.Infrastructures.Interfaces;
using BASEAPP.UI.Infrastructures.Services;
using BASEAPP.UI.ViewModels;
using BASEAPP.UI.ViewModels.Shopping;
using BASEAPP.UI.Views;
using BASEAPP.UI.Views.Shopping;
using Prism.Ioc;

namespace BASEAPP.UI
{
    internal static class ServiceRegister
    {
        public static void UseDialogs(this IContainerRegistry containerRegistry)
        {

        }

        public static void UseNavigaions(this IContainerRegistry containerRegistry)
        {

            // View
            containerRegistry.RegisterForNavigation<MainWindow, MainWindowViewModel>();
            containerRegistry.RegisterForNavigation<Shopping, ShoppingViewModel>();


        }

        public static void UseServices(this IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IProductService, ProductService>();
        }
    }
}
