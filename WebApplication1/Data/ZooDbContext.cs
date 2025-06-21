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
            new Category { Id = 2, Name = "Vogels" },
            new Category { Id = 3, Name = "Reptielen" },
            new Category { Id = 4, Name = "Amfibieën" },
            new Category { Id = 5, Name = "Vissen" },
            new Category { Id = 6, Name = "Insecten" },
            new Category { Id = 7, Name = "Schaaldieren" },
            new Category { Id = 8, Name = "Weekdieren" },
            new Category { Id = 9, Name = "Kringloopdieren" },
            new Category { Id = 10, Name = "Buideldieren" },
            new Category { Id = 11, Name = "Pinnipedia" },
            new Category { Id = 12, Name = "Carnivoren" },
            new Category { Id = 13, Name = "Herbivoren" },
            new Category { Id = 14, Name = "Omnivoren" },
            new Category { Id = 15, Name = "Primaten" },
            new Category { Id = 16, Name = "Carnivora" },
            new Category { Id = 17, Name = "Cetacea" },
            new Category { Id = 18, Name = "Chiroptera" },
            new Category { Id = 19, Name = "Rodentia" },
            new Category { Id = 20, Name = "Artiodactyla" }
        );

        modelBuilder.Entity<Zoo>().HasData(
            new Zoo { Id = 1, Name = "Dierentuin Amsterdam", Location = "Amsterdam" },
            new Zoo { Id = 2, Name = "Ouwehands Dierenpark", Location = "Rhenen" },
            new Zoo { Id = 3, Name = "Artis Royal Zoo", Location = "Amsterdam" },
            new Zoo { Id = 4, Name = "Burgers' Zoo", Location = "Arnhem" },
            new Zoo { Id = 5, Name = "Safaripark Beekse Bergen", Location = "Hilvarenbeek" },
            new Zoo { Id = 6, Name = "Blijdorp", Location = "Rotterdam" },
            new Zoo { Id = 7, Name = "Zoo Rotterdam", Location = "Rotterdam" },
            new Zoo { Id = 8, Name = "Wilhelma", Location = "Stuttgart" },
            new Zoo { Id = 9, Name = "Tiergarten Schönbrunn", Location = "Wenen" },
            new Zoo { Id = 10, Name = "London Zoo", Location = "Londen" },
            new Zoo { Id = 11, Name = "Berlin Zoologischer Garten", Location = "Berlijn" },
            new Zoo { Id = 12, Name = "Prague Zoo", Location = "Praag" },
            new Zoo { Id = 13, Name = "ZooParc de Beauval", Location = "France" },
            new Zoo { Id = 14, Name = "San Diego Zoo", Location = "San Diego" },
            new Zoo { Id = 15, Name = "Bronx Zoo", Location = "New York" },
            new Zoo { Id = 16, Name = "Taronga Zoo", Location = "Sydney" },
            new Zoo { Id = 17, Name = "Singapore Zoo", Location = "Singapore" },
            new Zoo { Id = 18, Name = "Copenhagen Zoo", Location = "Kopenhagen" },
            new Zoo { Id = 19, Name = "Beijing Zoo", Location = "Beijing" },
            new Zoo { Id = 20, Name = "Edinburgh Zoo", Location = "Edinburgh" }
        );

        

        modelBuilder.Entity<Animal>().HasData(
            new Animal { Id = 1, Name = "Leeuw", Prey = "Gazelle", Species = "Panthera leo", CategoryId = 12 },
            new Animal { Id = 2, Name = "Olifant", Prey = "Planten", Species = "Loxodonta africana", CategoryId = 13 },
            new Animal { Id = 3, Name = "Papegaai", Prey = "Zaden", Species = "Ara ararauna", CategoryId = 2 },
            new Animal { Id = 4, Name = "Boa", Prey = "Knaagdieren", Species = "Boa constrictor", CategoryId = 3 },
            new Animal { Id = 5, Name = "Kikker", Prey = "Insecten", Species = "Rana temporaria", CategoryId = 4 },
            new Animal { Id = 6, Name = "Goudvis", Prey = "Algen", Species = "Carassius auratus", CategoryId = 5 },
            new Animal { Id = 7, Name = "Vlinder", Prey = "Nectar", Species = "Danaus plexippus", CategoryId = 6 },
            new Animal { Id = 8, Name = "Kreeft", Prey = "Detritus", Species = "Homarus gammarus", CategoryId = 7 },
            new Animal { Id = 9, Name = "Inktvis", Prey = "Visjes", Species = "Octopus vulgaris", CategoryId = 8 },
            new Animal { Id = 10, Name = "Oehoe", Prey = "Muizen", Species = "Bubo bubo", CategoryId = 2 },
            new Animal { Id = 11, Name = "Pinguïn", Prey = "Vis", Species = "Aptenodytes forsteri", CategoryId = 2 },
            new Animal { Id = 12, Name = "Giraffe", Prey = "Bladeren", Species = "Giraffa camelopardalis", CategoryId = 13 },
            new Animal { Id = 13, Name = "Chimpansee", Prey = "Fruit", Species = "Pan troglodytes", CategoryId = 15 },
            new Animal { Id = 14, Name = "Walvis", Prey = "Krill", Species = "Balaenoptera musculus", CategoryId = 17 },
            new Animal { Id = 15, Name = "Vleermuis", Prey = "Insecten", Species = "Myotis myotis", CategoryId = 18 },
            new Animal { Id = 16, Name = "Muis", Prey = "Zaden", Species = "Mus musculus", CategoryId = 19 },
            new Animal { Id = 17, Name = "Hert", Prey = "Planten", Species = "Cervus elaphus", CategoryId = 20 },
            new Animal { Id = 18, Name = "Koe", Prey = "Gras", Species = "Bos taurus", CategoryId = 20 },
            new Animal { Id = 19, Name = "Wolf", Prey = "Herten", Species = "Canis lupus", CategoryId = 12 },
            new Animal { Id = 20, Name = "Eend", Prey = "Kleine vis", Species = "Anas platyrhynchos", CategoryId = 2 }
        );
    }
}


}
