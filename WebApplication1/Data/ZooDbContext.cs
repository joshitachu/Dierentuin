using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

using Micusing Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ZooDbContext : DbContext
    {
        private string connectionString = "Server=(localdb)\\mssqllocaldb;Database=Dierentuin1;Trusted_Connection=True;MultipleActiveResultSets=true";

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Enclosure> Enclosures { get; set; }
        public DbSet<Zoo> Zoos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Voeg eventueel aanvullende configuratie toe hier, zoals relaties, indexen, etc.
            base.OnModelCreating(modelBuilder);
        }
    }
}
