using SNS.API.Core.IRepositories;

namespace SNS.API.Core.IConfiguration
{
    public interface IUnitOfWork
    {
        IMatchedNewsRepository MatchedNews { get; }

        Task CompleteAsync();
    }
}
