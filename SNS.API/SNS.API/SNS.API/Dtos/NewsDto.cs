using SNS.API.Data.Entitites;

namespace SNS.API.Dtos
{
    public class NewsDto
    {
        public NewsWebsite? NewsWebsite { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
