using BASEAPP.BaseAPI.Response;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs.Cart;
using BASEAPP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BASEAPP.BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepo;
        private ResponseDto<List<CartItemDto>> _response;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepo)
        {
            _shoppingCartRepo = shoppingCartRepo;
            _response = new ResponseDto<List<CartItemDto>>();
        }

        [HttpGet]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> Get()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    var shoppingCart = await _shoppingCartRepo.GetCartItems(userId);

                    if (shoppingCart == null)
                    {
                        _response.Message = "No item found.";
                        _response.IsSuccess = false;
                        return NotFound(_response);
                    }

                    _response.IsSuccess = true;
                    _response.Result = shoppingCart;
                    return Ok(_response);
                }
                else
                {
                    _response.Message = "User ID not found in claims or invalid.";
                    return BadRequest(_response);
                }
            }
            catch (Exception ex)
            {
                _response.Message = $"Internal Server Error: {ex.Message}";
                _response.IsSuccess = false;
                return StatusCode(500, _response);
            }
        }

        [HttpPost("add-product")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<ActionResult<bool>> AddProductToCart([FromBody] AddToCartDto addToCartDto)
        {
            var reponse = new ResponseDto<bool>();
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

                if (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out Guid userId))
                {
                    var isAdded = await _shoppingCartRepo.AddProductToCart(userId, addToCartDto);

                    if (isAdded)
                    {
                        var shoppingCart = await _shoppingCartRepo.GetShoppingCart(userId);
                        reponse.IsSuccess = true;
                        reponse.Result = true;
                        return Ok(reponse);
                    }
                    else
                    {
                        reponse.Message = "Failed to add product to the cart.";
                        reponse.IsSuccess = false;
                        return BadRequest(reponse);
                    }
                }
                else
                {
                    reponse.Message = "User ID not found in claims or invalid.";
                    return BadRequest(reponse);
                }
            }
            catch (Exception ex)
            {
                reponse.Message = $"Internal Server Error: {ex.Message}";
                reponse.IsSuccess = false;
                return StatusCode(500, reponse);
            }
        }
    }
}
