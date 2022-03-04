using SNS.API.Core.IRepositories;

namespace SNS.API.Core.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly SnsDbContext _context;       
        protected readonly ILogger _logger;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(SnsDbContext context, ILogger logger)
        {
            _context = context;
            _logger = logger;
            _dbSet = context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            T entity = await GetById(id);

            _dbSet.Remove(entity);

        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
           return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

    }
}
