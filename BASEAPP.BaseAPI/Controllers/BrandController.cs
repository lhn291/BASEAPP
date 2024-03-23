using BASEAPP.BaseAPI.Response;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.Models;
using BASEAPP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BASEAPP.BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandRepository _brandRepo;
        private ResponseDto<Brand> _response;

        public BrandController(IBrandRepository brandRepo)
        {
            _brandRepo = brandRepo ?? throw new ArgumentNullException(nameof(brandRepo));
            _response = new ResponseDto<Brand>();
        }

        // GET api/Brand
        [HttpGet]
        public async Task<ActionResult<List<Brand>>> Get()
        {
            ResponseDto<List<Brand>> response = new ResponseDto<List<Brand>>();
            try
            {
                var brands = await _brandRepo.GetAll();

                if (brands == null || brands.Count == 0)
                {
                    response.Message = "No brands found.";
                    response.IsSuccess = false;
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Result = brands;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = $"Internal Server Error: {ex.Message}";
                response.IsSuccess = false;
                return StatusCode(500, response);
            }
        }

        // GET api/Brand/name
        [HttpGet("{name}")]
        public async Task<ActionResult<Brand>> Get(string name)
        {
            try
            {
                var brand = await _brandRepo.GetByName(name);

                if (brand == null)
                {
                    _response.Message = $"Brand with name {name} not found";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = brand;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = $"Internal Server Error: {ex.Message}";
                _response.IsSuccess = false;
                return StatusCode(500, _response);
            }
        }

        // GET api/Brand/id/{brandId}
        [HttpGet("id/{brandId}")]
        public async Task<ActionResult<Brand>> Get(int? brandId)
        {
            try
            {
                if (brandId == null)
                {
                    _response.Message = "Brand ID cannot be null.";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var brand = await _brandRepo.GetById(brandId.Value);

                if (brand == null)
                {
                    _response.Message = $"Brand with id {brandId} not found";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = brand;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = $"Internal Server Error: {ex.Message}";
                _response.IsSuccess = false;
                return StatusCode(500, _response);
            }
        }

        // POST api/Brand
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<int>> Post([FromBody] Brand brand)
        {
            ResponseDto<int> response = new ResponseDto<int>();
            try
            {
                if (brand == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Brand object";
                    return BadRequest(response);
                }

                bool brandExists = await _brandRepo.Exists(brand);

                if (brandExists)
                {
                    response.IsSuccess = false;
                    response.Message = "Brand already exists";
                    return Conflict(response);
                }

                var brandId = await _brandRepo.Create(brand);
                response.Result = brandId;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Internal Server Error: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        // PUT api/Brand/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Brand brand)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();
            try
            {
                if (brand == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Brand object.";
                    return BadRequest(response);
                }

                var result = await _brandRepo.Update(id, brand);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = $"Brand with ID {id} not found.";
                    return NotFound(response);
                }

                response.Result = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Internal Server Error: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        // DELETE api/Brand/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();
            try
            {
                var result = await _brandRepo.Delete(id);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = $"Brand with ID {id} not found.";
                    return NotFound(response);
                }

                response.Result = result;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Internal Server Error: {ex.Message}";
                return StatusCode(500, response);
            }
        }
    }
}
