using BASEAPP.BaseAPI.Response;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs;
using BASEAPP.Models.DTOs.Product;
using BASEAPP.Models.DTOs.User;
using BASEAPP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BASEAPP.BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepop;
        private ResponseDto<ProductDto> _response;

        public ProductController(IProductRepository productRepo)
        {
            _productRepop = productRepo ?? throw new ArgumentNullException(nameof(productRepo));
            _response = new ResponseDto<ProductDto>();
        }

        // GET api/Product
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page = 1,
                                             [FromQuery] int pageSize = 20)
        {
            ResponseDto<PagedResultDto<ProductDto>> response = new ResponseDto<PagedResultDto<ProductDto>>();
            try
            {
                var products = await _productRepop.GetProductsAsync(page, pageSize);

                if (products == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "Products not found ";
                    return NotFound(response);
                }
                
                response.Result = products;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        // GET api/Product/name
        [HttpGet("{name}")]
        public async Task<ActionResult<List<ProductDto>>> Get(string name)
        {
            ResponseDto<List<ProductDto>> response = new ResponseDto<List<ProductDto>>();
            try
            {
                if (name == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Product object";
                    return BadRequest(response);
                }

                var products = await _productRepop.GetByName(name);

                if (products == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product not found";
                    return NotFound(response);
                }

                response.Result = products;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Internal Server Error: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        // POST api/Product
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<int>> Post([FromBody] ProductCreateDto Product)
        {
            ResponseDto<int> response = new ResponseDto<int>();
            try
            {
                if (Product == null)
                {
                    return BadRequest("Invalid Product object");
                }

                var ProductCreated = await _productRepop.CreateProductAsync(Product);

                if (ProductCreated == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Create product unsuccess";
                    response.Result = 0;
                    return Ok(response);
                }

                response.Result = ProductCreated.Id;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        // PUT api/Product/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] ProductUpdateDto productUpdateDto)
        {
            try
            {
                var success = await _productRepop.UpdateProductAsync(id, productUpdateDto);

                if (success)
                {
                    _response.Message = "Updated";
                    return Ok(_response);
                }
                else
                {
                    _response.Message = "Product not found";
                    return NotFound(_response);
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        // DELETE api/Product/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var result = await _productRepop.Delete(id);
                if (!result)
                {
                    return NotFound($"Product with id {id} not found");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }   
        }

        // GET api/Product/top-rated
        [HttpGet("top-rated")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> GetTopRatedProducts([FromQuery] int page = 1,
                                                             [FromQuery] int pageSize = 20)
        {
            ResponseDto<PagedResultDto<ProductDto>> response = new ResponseDto<PagedResultDto<ProductDto>>();
            try
            {
                var topRatedProducts = await _productRepop.GetTopRatedProductsAsync(page, pageSize);

                if (topRatedProducts == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "No top-rated products found.";
                    return NotFound(response);
                }

                response.Result = topRatedProducts;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        // GET api/Product/price-descending
        [HttpGet("price-descending")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> GetProductsByPriceDescending([FromQuery] int page = 1,
                                                                      [FromQuery] int pageSize = 20)
        {
            ResponseDto<PagedResultDto<ProductDto>> response = new ResponseDto<PagedResultDto<ProductDto>>();
            try
            {
                var products = await _productRepop.GetProductsByPriceDescendingAsync(page, pageSize);

                if (products == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "No products found by price (descending).";
                    return NotFound(response);
                }

                response.Result = products;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        // GET api/Product/price-ascending
        [HttpGet("price-ascending")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> GetProductsByPriceAscending([FromQuery] int page = 1,
                                                                     [FromQuery] int pageSize = 20)
        {
            ResponseDto<PagedResultDto<ProductDto>> response = new ResponseDto<PagedResultDto<ProductDto>>();
            try
            {
                var products = await _productRepop.GetProductsByPriceAscendingAsync(page, pageSize);

                if (products == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "No products found by price (ascending).";
                    return NotFound(response);
                }

                response.Result = products;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        // GET api/Product/price-range
        [HttpGet("price-range")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> GetProductsInPriceRange([FromQuery] double minPrice,
                                                                 [FromQuery] double maxPrice,
                                                                 [FromQuery] int page = 1,
                                                                 [FromQuery] int pageSize = 20)
        {
            ResponseDto<PagedResultDto<ProductDto>> response = new ResponseDto<PagedResultDto<ProductDto>>();
            try
            {
                var products = await _productRepop.GetProductsInPriceRangeAsync(minPrice, maxPrice, page, pageSize);

                if (products == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = "No products found in the price range.";
                    return NotFound(response);
                }

                response.Result = products;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        // GET api/Product/category/{categoryName}
        [HttpGet("category/{categoryName}")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> GetProductsByCategory(string categoryName,
                                                               [FromQuery] int page = 1,
                                                               [FromQuery] int pageSize = 20)
        {
            ResponseDto<PagedResultDto<ProductDto>> response = new ResponseDto<PagedResultDto<ProductDto>>();
            try
            {
                var products = await _productRepop.GetProductsByCategoryAsync(categoryName, page, pageSize);

                if (products == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = $"No products found in category '{categoryName}'.";
                    return NotFound(response);
                }

                response.Result = products;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }

        // GET api/Product/brand/{brandName}
        [HttpGet("brand/{brandName}")]
        [Authorize(Roles = AppRole.Customer)]
        public async Task<IActionResult> GetProductsByBrand(string brandName,
                                                            [FromQuery] int page = 1,
                                                            [FromQuery] int pageSize = 20)
        {
            ResponseDto<PagedResultDto<ProductDto>> response = new ResponseDto<PagedResultDto<ProductDto>>();
            try
            {
                var products = await _productRepop.GetProductsByBrandAsync(brandName, page, pageSize);

                if (products == null)
                {
                    response.Result = null;
                    response.IsSuccess = false;
                    response.Message = $"No products found for brand '{brandName}'.";
                    return NotFound(response);
                }

                response.Result = products;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
                response.IsSuccess = false;
                return BadRequest(response);
            }
        }
    }
}
