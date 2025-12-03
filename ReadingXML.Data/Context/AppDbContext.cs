using Microsoft.EntityFrameworkCore;
using ReadingXML.Domain.Entities;

namespace ReadingXML.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<XMLExtract> XMLExtract { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<XMLExtract>()
              .HasIndex(s => s.IdXMLExtract)
              .IsUnique();
        }
    }
}
