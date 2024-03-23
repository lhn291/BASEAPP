using BASEAPP.UI.Infrastructures.Interfaces;
using BASEAPP.UI.Models.Database;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace BASEAPP.UI.ViewModels.Shopping
{
    public class ShoppingViewModel : BindableBase
    {
        private IProductService _productService;

        private ObservableCollection<Product> _lsProduct = new ObservableCollection<Product>();
        public ObservableCollection<Product> LsProduct
        {
            get { return _lsProduct; }
            set { SetProperty(ref _lsProduct, value); }
        }

        private int pageSize = 12;

        private string _productName;
        public string ProductName
        {
            get { return _productName; }
            set { SetProperty(ref _productName, value); }
        }

        private int _pageCount;
        public int PageCount
        {
            get { return _pageCount; }
            set { SetProperty(ref _pageCount, value); }
        }

        private int _pageIndex = 1;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { SetProperty(ref _pageIndex, value); }
        }

        public DelegateCommand SearchCommand { get; set; }

        public DelegateCommand PageCLickCommand { get; set; }

        public ShoppingViewModel(IProductService productService)
        {
            _productService = productService;
            GetTotalProductExecute();
            ShowProductExecute();
            SearchCommand = new DelegateCommand(SearchProductExecute);
            PageCLickCommand = new DelegateCommand(PageCLickExecute);
        }

        private void SearchProductExecute() {
            LsProduct.Clear();

            if (string.IsNullOrEmpty(ProductName))
            {
                ShowProductExecute();
            }

            Task.Run(async () => {
                var products = await _productService.GetProductsByName(ProductName);
                LsProduct = new ObservableCollection<Product>(products);
            });
        }

        private void ShowProductExecute()
        {
            LsProduct.Clear();

            Task.Run(async () => {
                var products = await _productService.GetALlProducts(PageIndex, pageSize);
                LsProduct = new ObservableCollection<Product>(products);
            });
        }

        private void GetTotalProductExecute()
        {
            LsProduct.Clear();

            Task.Run(async () => {
                var totalItems = await _productService.GetALlItemProducts();
                if (totalItems % pageSize == 0)
                {
                    PageCount = totalItems / pageSize;
                    return;
                }
                PageCount = totalItems / pageSize;
                PageCount++;
            });
        }

        private void PageCLickExecute()
        {
            var pagecheck = PageIndex;
            ShowProductExecute();
        }

    }
}
