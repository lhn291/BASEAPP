using BASEAPP.Models.Models;
using Microsoft.AspNetCore.Identity;

namespace BASEAPP.DataAccess.Repository
{
    public interface IAuthRepository
    {
        public Task<IdentityResult> SignUpAsync(SignUpModel model);
        public Task<string> SignInAsync(SignInModel model);
    }
}
