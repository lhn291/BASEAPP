using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.DataAccess.Repository
{

    public class CategoryRepository : Repository<Category, int>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Category> GetByName(string name)
        {
            var category = await _db.Categories.FirstOrDefaultAsync(c => c.Name.Equals(name));
            return category!;
        }
    }
}
