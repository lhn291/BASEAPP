using BASEAPP.BaseAPI.Response;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs.ProductReview;
using BASEAPP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BASEAPP.BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly IProductReviewRepository _productReviewRepo;
        private ResponseDto<List<ProductReviewDto>> _response;

        public ProductReviewController(IProductReviewRepository productReviewRepo)
        {
            _productReviewRepo = productReviewRepo ?? throw new ArgumentNullException(nameof(productReviewRepo));
            _response = new ResponseDto<List<ProductReviewDto>>();
        }

        // GET api/productreview
        // Endpoint for retrieving product reviews by product ID
        [HttpGet]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var productReviews = await _productReviewRepo.GetProductReviewsAsync(id);

                if (productReviews == null || productReviews.Count == 0)
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = "No product reviews found for the given product.";
                    return NotFound(_response);
                }

                _response.Result = productReviews;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        // POST api/productreview
        // Endpoint for creating a new product review
        [HttpPost]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> Create([FromBody] ProductReviewCreateDto reviewDto)
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
                {
                    _response.Message = "User ID not found in claims.";
                    return BadRequest(_response);
                }

                int reviewId = await _productReviewRepo.CreateProductReviewAsync(reviewDto, Guid.Parse(userIdClaim.Value));
                _response.Result = null;
                _response.Message = "Created !";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
    }
}
