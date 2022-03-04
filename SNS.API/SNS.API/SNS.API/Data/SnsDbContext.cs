using SNS.API.Data.Entitites;

namespace SNS.API.Data
{
    public class SnsDbContext: DbContext
    {

        public SnsDbContext(DbContextOptions<SnsDbContext> options) : base(options) { }
        public DbSet<News> News { get; set; }
        public DbSet<MatchedNews> MatchedNews { get; set; }
        public DbSet<NewsWebsite>  NewsWebsites{ get; set; }
        public DbSet<FiltredWord> FiltredWords { get; set; }
        public DbSet<MatchedWord> MatchedWords { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MatchedNews>()
                .HasOne(e => e.NewsOne)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MatchedNews>().
                HasOne(e => e.NewsTwo)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
