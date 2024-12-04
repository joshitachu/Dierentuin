using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;



public class ZooDbContext : DbContext
{

    private String connectionString =
        "Server=(localdb)\\mssqllocaldb;Database=Dierentuin2;Trusted_Connection=True;MultipleActiveResultSets=true";
    
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Enclosure> Enclosures { get; set; }
    public DbSet<Zoo> Zoos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connectionString);
    }
    
    
    
}