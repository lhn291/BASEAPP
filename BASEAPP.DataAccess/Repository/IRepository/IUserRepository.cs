using BASEAPP.Models.DTOs;
using BASEAPP.Models.Models;

namespace BASEAPP.DataAccess.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<PagedResultDto<ApplicationUser>> GetUsersAsync(int page, int pageSize);
    }
}