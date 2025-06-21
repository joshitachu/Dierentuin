using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;


namespace WebApplication1.Data
{
    public class ZooDbContext : DbContext
    {

        public ZooDbContext(DbContextOptions<ZooDbContext> options)
                : base(options)
        {

        }

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
    base.OnModelCreating(modelBuilder);

    modelBuilder.Entity<Category>().HasData(
        new Category { Id = 1, Name = "Zoogdieren" },
        new Category { Id = 2, Name = "Vogels" }
    );

    modelBuilder.Entity<Animal>().HasData(
    new Animal { Id = 1, Name = "Leeuw", Prey = "Gazelle", Species = "Panthera leo", CategoryId = 1 }
    
);
} 
        
    }
}
