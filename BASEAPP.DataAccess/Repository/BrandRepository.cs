using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository.IRepository;
using BASEAPP.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.DataAccess.Repository
{
    public class BrandRepository : Repository<Brand, int>, IBrandRepository
    {
        public BrandRepository(AppDbContext db) : base(db)
        {
        }

        public async Task<Brand> GetByName(string name)
        {
            var brand = await _db.Brands.FirstOrDefaultAsync(c => c.Name.Equals(name));
            return brand!;
        }
    }
}