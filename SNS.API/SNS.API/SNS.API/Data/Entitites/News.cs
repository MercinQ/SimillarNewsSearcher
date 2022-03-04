using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNS.API.Data.Entitites
{
    public class News
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NewsWebsiteId { get; set; }
        public NewsWebsite? NewsWebsite { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
    }
}
