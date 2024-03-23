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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private ResponseDto<Category> _response;

        public CategoryController(ICategoryRepository CategoryRepo)
        {
            _categoryRepo = CategoryRepo ?? throw new ArgumentNullException(nameof(CategoryRepo));
            _response = new ResponseDto<Category>();
        }

        // GET api/Category
        [HttpGet]
        public async Task<ActionResult<List<Category>>> Get()
        {
            ResponseDto<List<Category>> response = new ResponseDto<List<Category>>();
            try
            {
                var categorys = await _categoryRepo.GetAll();

                if (categorys == null || categorys.Count == 0)
                {
                    response.Message = "No Categorys found.";
                    response.IsSuccess = false;
                    return NotFound(response);
                }

                response.IsSuccess = true;
                response.Result = categorys;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Message = $"Internal Server Error: {ex.Message}";
                response.IsSuccess = false;
                return StatusCode(500, response);
            }
        }

        // GET api/Category/name
        [HttpGet("{name}")]
        public async Task<ActionResult<Category>> Get(string name)
        {
            try
            {
                var category = await _categoryRepo.GetByName(name);

                if (category == null)
                {
                    _response.Message = $"Category with name {name} not found";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }

                _response.IsSuccess = true;
                _response.Result = category;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = $"Internal Server Error: {ex.Message}";
                _response.IsSuccess = false;
                return StatusCode(500, _response);
            }
        }

        // GET api/Category/id/{CategoryId}
        [HttpGet("id/{CategoryId}")]
        public async Task<ActionResult<Category>> Get(int? categoryId)
        {
            try
            {
                if (categoryId == null)
                {
                    _response.Message = "Category ID cannot be null.";
                    _response.IsSuccess = false;
                    return BadRequest(_response);
                }

                var category = await _categoryRepo.GetById(categoryId.Value);

                if (category == null)
                {
                    _response.Message = $"Category with id {categoryId} not found";
                    _response.IsSuccess = false;
                    return NotFound(_response);
                }
                _response.IsSuccess = true;
                _response.Result = category;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = $"Internal Server Error: {ex.Message}";
                _response.IsSuccess = false;
                return StatusCode(500, _response);
            }
        }

        // POST api/Category
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<int>> Post([FromBody] Category category)
        {
            ResponseDto<int> response = new ResponseDto<int>();
            try
            {
                if (category == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Category object";
                    return BadRequest(response);
                }

                bool CategoryExists = await _categoryRepo.Exists(category);

                if (CategoryExists)
                {
                    response.IsSuccess = false;
                    response.Message = "Category already exists";
                    return Conflict(response);
                }

                var categoryId = await _categoryRepo.Create(category);
                response.Result = categoryId;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Internal Server Error: {ex.Message}";
                return StatusCode(500, response);
            }
        }

        // PUT api/Category/{id}
        [HttpPut("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<bool>> Put(int id, [FromBody] Category category)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();
            try
            {
                if (category == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Invalid Category object.";
                    return BadRequest(response);
                }

                var result = await _categoryRepo.Update(id, category);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = $"Category with ID {id} not found.";
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

        // DELETE api/Category/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            ResponseDto<bool> response = new ResponseDto<bool>();
            try
            {
                var result = await _categoryRepo.Delete(id);
                if (!result)
                {
                    response.IsSuccess = false;
                    response.Message = $"Category with ID {id} not found.";
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
