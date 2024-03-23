using AutoMapper;
using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.DTOs;
using BASEAPP.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.DataAccess.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        private IMapper _mapper;

        public UserRepository(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<ApplicationUser>> GetUsersAsync(int page, int pageSize)
        {
            try
            {
                var users = await _db.Users
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var totalUsers = await _db.Users.CountAsync();

                var pagedResult = new PagedResultDto<ApplicationUser>
                {
                    TotalItems = totalUsers,
                    Items = users
                };

                return pagedResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching users.", ex);
            }
        }
    }
}
