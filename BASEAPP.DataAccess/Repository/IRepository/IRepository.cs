namespace BASEAPP.DataAccess.Repository.IRepository
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<T> GetById(TKey id);
        Task<List<T>> GetAll();
        Task<int> Create(T entity);
        Task<bool> Update(TKey id, T entity);
        Task<bool> Delete(TKey id);
        Task<bool> Exists(T entity);
    }
}
