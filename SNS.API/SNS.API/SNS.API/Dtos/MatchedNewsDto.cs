using SNS.API.Data.Entitites;

namespace SNS.API.Dtos
{
    public class MatchedNewsDto
    {
        public NewsDto? NewsOne { get; set; }

        public NewsDto? NewsTwo { get; set; }

        public int MatchingPercent { get; set; }
    }
}
