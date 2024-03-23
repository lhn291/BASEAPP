using BASEAPP.Models.Models;

namespace BASEAPP.DataAccess.Repository.IRepository
{
    public interface IBrandRepository : IRepository<Brand, int>
    {
        Task<Brand> GetByName(string name);
    }
}
