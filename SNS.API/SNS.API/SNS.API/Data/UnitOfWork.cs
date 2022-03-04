using SNS.API.Core.IConfiguration;
using SNS.API.Core.IRepositories;
using SNS.API.Core.Repositories;

namespace SNS.API.Data
{
    public class UnitOfWork: IUnitOfWork, IDisposable
    {
        private readonly SnsDbContext _context;
        private readonly ILogger _logger;

        public UnitOfWork(SnsDbContext context, ILoggerFactory loggerFactory)
        {
            _context = context;

            //pass here context and logger to repos
            MatchedNews = new MatchedNewsRepository(_context, _logger);
        }

        public IMatchedNewsRepository MatchedNews { get; private set; }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }
    }
}
