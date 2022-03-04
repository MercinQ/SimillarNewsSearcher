using SNS.API.Core.IConfiguration;
using SNS.API.Data.Entitites;
using SNS.API.Dtos;
using SNS.API.Services.Interfaces;

namespace SNS.API.Services
{
    public class MatchedNewsService: IMatchedNewsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MatchedNewsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<MatchedNewsDto> GenerateMatchedNews()
        {
            throw new NotImplementedException();
        }

        public async Task SaveMatchedNews(MatchedNewsDto dto)
        {
            var matchedNews = new MatchedNews
            {
                MatchingPercent = dto.MatchingPercent,
                RowAddedDate = new DateTime(),
                NewsOne = new News
                {
                    Content = dto.NewsOne.Content,
                    Title = dto.NewsOne.Title,
                    NewsWebsite = new NewsWebsite
                    {
                        Url = dto.NewsOne.NewsWebsite.Url
                    }
                },
                NewsTwo = new News
                {
                    Content = dto.NewsTwo.Content,
                    Title = dto.NewsTwo.Title,
                    NewsWebsite = new NewsWebsite
                    {
                        Url = dto.NewsTwo.NewsWebsite.Url
                    }
                }
            };

            await _unitOfWork.MatchedNews.AddAsync(matchedNews);

            await _unitOfWork.CompleteAsync();                   
        }
    }
}
