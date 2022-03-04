namespace SNS.API.Core.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(Guid id);
        Task AddAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
