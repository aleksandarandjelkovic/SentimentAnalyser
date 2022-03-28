using Microsoft.EntityFrameworkCore;
using SentimentAnalyserAPI.Entities;

namespace SentimentAnalyserAPI.DBContext
{
    /// <summary>
    /// AppDBContext context
    /// </summary>
    public class AppDBContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AppDBContext"/> class.
        /// </summary>
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        /// <summary>
        /// Creating entity model
        /// </summary>
        /// <param name="modelBuilder">Model builder</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Word>()
                .ToTable("Lexicon")
                .HasKey(x => x.WordId);
        }

        /// <summary>
        /// Gets or sets Words DB set
        /// </summary>
        public DbSet<Word> Words { get; set; }
    }
}
