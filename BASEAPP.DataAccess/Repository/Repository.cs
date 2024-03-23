using BASEAPP.DataAccess.Data;
using BASEAPP.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BASEAPP.DataAccess.Repository
{
    public class Repository<T, TKey> : IRepository<T, TKey> where T : class
    {
        public readonly AppDbContext _db;

        public Repository(AppDbContext db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public virtual async Task<T> GetById(TKey id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            return entity!;
        } 

        public virtual async Task<List<T>> GetAll()
        {
            return await _db.Set<T>().ToListAsync();
        }

        public virtual async Task<int> Create(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
            await _db.SaveChangesAsync();

            var idProperty = entity.GetType().GetProperty("Id");
            if (idProperty != null)
            {
                var idValue = idProperty.GetValue(entity);
                return idValue != null ? Convert.ToInt32(idValue) : throw new InvalidOperationException("Id property is null");
            }
            else
            {
                throw new InvalidOperationException("Entity does not have an Id property");
            }
        }

        public virtual async Task<bool> Update(TKey id, T entity)
        {
            var existingEntity = await _db.Set<T>().FindAsync(id);
            if (existingEntity == null)
            {
                return false;
            }

            _db.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> Delete(TKey id)
        {
            var entity = await _db.Set<T>().FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            _db.Set<T>().Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }

        public virtual async Task<bool> Exists(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            }

            var entry = await _db.Set<T>().FirstOrDefaultAsync(e => e.Equals(entity));
            return entry != null;
        }
    }
}
