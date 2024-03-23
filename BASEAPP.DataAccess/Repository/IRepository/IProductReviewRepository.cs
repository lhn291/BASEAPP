using BASEAPP.Models.DTOs.ProductReview;
using BASEAPP.Models.Models;

namespace BASEAPP.DataAccess.Repository.IRepository
{
    public interface IProductReviewRepository : IRepository<ProductReview, int>
    {
        Task<List<ProductReviewDto>> GetProductReviewsAsync(int productId);
        Task<int> CreateProductReviewAsync(ProductReviewCreateDto reviewDto, Guid userId);
    }
}
