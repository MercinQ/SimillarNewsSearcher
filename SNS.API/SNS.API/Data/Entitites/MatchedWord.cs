using System.ComponentModel.DataAnnotations;

namespace SNS.API.Data.Entitites
{
    public class MatchedWord
    {
        [Key]
        public long Id { get; set; }
        public long MatchedNewsId { get; set; }
        public MatchedNews? MatchedNews { get; set; }
        public string? Value { get; set; }
    }
}
