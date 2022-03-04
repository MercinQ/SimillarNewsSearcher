using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SNS.API.Data.Entitites
{
    public class MatchedNews
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }


        [ForeignKey("NewsOneId")]
        public virtual News? NewsOne { get; set; }
        public int NewsOneId { get; set; }
        

        [ForeignKey("NewsTwoId")]
        public virtual News? NewsTwo { get; set; }
        public int NewsTwoId { get; set; }

        public int MatchingPercent { get; set; }

        public DateTime? RowAddedDate { get; set; }
    }
}
