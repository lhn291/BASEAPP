using BASEAPP.Models.DTOs.User;
using BASEAPP.Models.DTOs;
using BASEAPP.Models.Models;
using BASEAPP.Models.DTOs.Product;

namespace BASEAPP.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product, int>
    {
        Task<List<ProductDto>> GetByName(string name);
        Task<PagedResultDto<ProductDto>> GetProductsAsync(int page, int pageSize);
        Task<Product> CreateProductAsync(ProductCreateDto productDto);
        Task<bool> UpdateProductAsync(int productId, ProductUpdateDto productDto);
        Task<PagedResultDto<ProductDto>> GetTopRatedProductsAsync(int page, int pageSize);
        Task<PagedResultDto<ProductDto>> GetProductsByPriceDescendingAsync(int page, int pageSize);
        Task<PagedResultDto<ProductDto>> GetProductsByPriceAscendingAsync(int page, int pageSize);
        Task<PagedResultDto<ProductDto>> GetProductsInPriceRangeAsync(double minPrice, double maxPrice, int page, int pageSize);
        Task<PagedResultDto<ProductDto>> GetProductsByCategoryAsync(string categoryName, int page, int pageSize);
        Task<PagedResultDto<ProductDto>> GetProductsByBrandAsync(string brandName, int page, int pageSize);
    }
}
