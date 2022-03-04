namespace SNS.API.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        void AddAsync(T entity);
        void DeleteAsync(Guid id);
    }
}
