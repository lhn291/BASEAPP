using BASEAPP.BaseAPI.Response;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs;
using BASEAPP.Models.Models;
using BASEAPP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BASEAPP.BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private ResponseDto<PagedResultDto<ApplicationUser>> _response;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
            _response = new ResponseDto<PagedResultDto<ApplicationUser>>();
        }

        // GET api/user
        // Endpoint for retrieving paginated list of users
        [HttpPost]
        [Authorize(Roles = AppRole.Admin)]
        public async Task<IActionResult> Get(int page = 1, int pageSize = 10)
        {
            try
            {
                var pagedResult = await _userRepo.GetUsersAsync(page, pageSize);

                if (pagedResult == null)
                {
                    _response.Result = null;
                    _response.IsSuccess = false;
                    _response.Message = "No users found.";
                    return NotFound(_response);
                }

                _response.Result = pagedResult;
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
