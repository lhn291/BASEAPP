using BASEAPP.UI.Models;
using BASEAPP.UI.Models.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BASEAPP.UI.Infrastructures.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetALlProducts(int page, int pageSize);

        Task<List<Product>> GetProductsByName(string name);

        Task<int> GetALlItemProducts();
    }
}
