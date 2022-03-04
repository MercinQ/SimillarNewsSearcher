using SNS.API.Core.IRepositories;
using SNS.API.Data.Entitites;

namespace SNS.API.Core.Repositories
{
    public class MatchedNewsRepository: GenericRepository<MatchedNews>, IMatchedNewsRepository
    {
        public MatchedNewsRepository(SnsDbContext context, ILogger logger) : base(context, logger)
        {
        }
    }
}
