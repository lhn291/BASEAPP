using BASEAPP.UI.Extensions;
using BASEAPP.UI.Infrastructures.Interfaces;
using BASEAPP.UI.Models;
using BASEAPP.UI.Models.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BASEAPP.UI.Infrastructures.Services
{

    public class ProductService : IProductService
    {
        private ApiClient _client;

        public ProductService()
        {
            _client = new ApiClient();
        }

        public async Task<List<Product>> GetALlProducts(int page , int pageSize)
        {
            var ProductList =  await _client.GetAsync<ResponseWithListItem<List<Product>>>($"api/Product?page={page}&pageSize={pageSize}");
            return ProductList.Result.Items.ToList();
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            var result = await _client.GetAsync<Response<List<Product>>>($"api/Product/{name}");
            return result.Result;
        }

        public async Task<int> GetALlItemProducts()
        {
            var ProductList = await _client.GetAsync<ResponseWithListItem<List<Product>>>($"api/Product?page={1}&pageSize={10}");
            int totalItems = ProductList.Result.TotalItems;
            return totalItems;
        }
    }
}
