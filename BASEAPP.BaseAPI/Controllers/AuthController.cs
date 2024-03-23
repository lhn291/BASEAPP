using BASEAPP.BaseAPI.Response;
using BASEAPP.DataAccess.Repository;
using BASEAPP.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace BASEAPP.BaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository accountRepo;
        private ResponseDto<bool> _response;

        public AuthController(IAuthRepository repo)
        {
            _response = new ResponseDto<bool>();
            accountRepo = repo;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpModel signUpModel)
        {
            var result = await accountRepo.SignUpAsync(signUpModel);
            if (result.Succeeded)
            {
                _response.Result = true;
                return Ok(_response);
            }

            _response.Result = false;
            return StatusCode(500);
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInModel signInModel)
        {
            var result = await accountRepo.SignInAsync(signInModel);

            if (string.IsNullOrEmpty(result))
            {
                return Unauthorized();
            }

            return Ok(result);
        }
    }
}
