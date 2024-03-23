using AutoMapper;
using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs.ProductReview;
using BASEAPP.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.DataAccess.Repository
{
    public class ProductReviewRepository : Repository<ProductReview, int>, IProductReviewRepository
    {
        private readonly IMapper _mapper;

        public ProductReviewRepository(AppDbContext db, IMapper mapper) : base(db)
        {
            _mapper = mapper;
        }

        public async Task<List<ProductReviewDto>> GetProductReviewsAsync(int productId)
        {
            try
            {
                var productReviews = await _db.ProductReviews
                    .Where(pr => pr.ProductId == productId)
                    .Include(pr => pr.User)
                    .Select(pr => new ProductReviewDto
                    {
                        Id = pr.Id,
                        ProductId = pr.ProductId,
                        Comment = pr.Comment,
                        Rating = pr.Rating,
                        ReviewDate = pr.ReviewDate,
                        UserName = pr.User.UserName
                    })
                    .ToListAsync();

                return productReviews;
            }
            catch (Exception ex)
            {
                var errorMessage = "An error occurred while fetching product reviews.";
                throw new Exception(errorMessage, ex);
            }
        }

        public async Task<int> CreateProductReviewAsync(ProductReviewCreateDto reviewDto, Guid userId)
        {
            try
            {
                if (userId == Guid.Empty || reviewDto.ProductId == 0)
                {
                    var errorMessage = "Not enough information provided for user or product.";
                    throw new Exception(errorMessage);
                }

                reviewDto.UserId = userId;
                var productReview = _mapper.Map<ProductReview>(reviewDto);
                _db.ProductReviews.Add(productReview);
                await _db.SaveChangesAsync();
                await UpdateProductAverageRatingAsync(reviewDto.ProductId);

                return productReview.Id;
            }
            catch (Exception ex)
            {
                var errorMessage = "Error occurred while creating a product review.";
                throw new Exception(errorMessage, ex);
            }
        }

        private async Task UpdateProductAverageRatingAsync(int productId)
        {
            var product = await _db.Products
                .Include(p => p.ProductReviews)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product != null)
            {
                product.AverageRating = product.ProductReviews.Any() ? product.ProductReviews.Average(pr => pr.Rating) : 0;
                await _db.SaveChangesAsync();
            }
        }
    }
}
