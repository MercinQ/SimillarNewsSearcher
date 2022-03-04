using SNS.API.Data.Entitites;
using SNS.API.Dtos;

namespace SNS.API.Services.Interfaces
{
    public interface IMatchedNewsService
    {
        public List<MatchedNewsDto> GenerateMatchedNews();

        public Task SaveMatchedNews(MatchedNewsDto dto);
    }
}
