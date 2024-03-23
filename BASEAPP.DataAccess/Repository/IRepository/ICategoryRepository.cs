using BASEAPP.Models.Models;

namespace BASEAPP.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category, int>
    {
        Task<Category> GetByName(string name);
    }
}
