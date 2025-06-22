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

            modelBuilder.Entity<Enclosure>().HasData(
    new Enclosure
    {
        Id = 101,
        Name = "Savannah Zone",
        Description = "Large grassland area for African animals.",
        Size = 1200.5,
        securityLevel = SecurityLevel.Medium,
        Habitat = Enclosure.HabitatType.Grassland,
        Climate = Enclosure.ClimateType.Tropical
    },
    new Enclosure
    {
        Id = 102,
        Name = "Penguin Cove",
        Description = "Cold environment for penguins.",
        Size = 800,
        securityLevel = SecurityLevel.High,
        Habitat = Enclosure.HabitatType.Aquatic,
        Climate = Enclosure.ClimateType.Arctic
    },
    new Enclosure
    {
        Id = 103,
        Name = "Desert Dunes",
        Description = "Hot and dry enclosure for desert species.",
        Size = 600,
        securityLevel = SecurityLevel.Low,
        Habitat = Enclosure.HabitatType.Desert,
        Climate = Enclosure.ClimateType.Tropical
    },
    new Enclosure
    {
        Id = 104,
        Name = "Rainforest Retreat",
        Description = "Dense forest area for tropical animals.",
        Size = 950,
        securityLevel = SecurityLevel.Medium,
        Habitat = Enclosure.HabitatType.Forest,
        Climate = Enclosure.ClimateType.Tropical
    },
    new Enclosure
    {
        Id = 105,
        Name = "Arctic Tundra",
        Description = "Chilly area for polar bears and arctic foxes.",
        Size = 1100,
        securityLevel = SecurityLevel.High,
        Habitat = Enclosure.HabitatType.Grassland,
        Climate = Enclosure.ClimateType.Arctic
    },
    new Enclosure
    {
        Id = 106,
        Name = "Jungle Jump",
        Description = "Lush forest enclosure for monkeys and birds.",
        Size = 720,
        securityLevel = SecurityLevel.Medium,
        Habitat = Enclosure.HabitatType.Forest,
        Climate = Enclosure.ClimateType.Tropical
    },
    new Enclosure
    {
        Id = 107,
        Name = "Giraffe Plains",
        Description = "Open space for giraffes and zebras.",
        Size = 1300,
        securityLevel = SecurityLevel.Low,
        Habitat = Enclosure.HabitatType.Grassland,
        Climate = Enclosure.ClimateType.Temperate
    },
    new Enclosure
    {
        Id = 108,
        Name = "Crocodile Creek",
        Description = "Watery habitat for crocodiles.",
        Size = 500,
        securityLevel = SecurityLevel.High,
        Habitat = Enclosure.HabitatType.Aquatic,
        Climate = Enclosure.ClimateType.Tropical
    },
    new Enclosure
    {
        Id = 109,
        Name = "Oasis Haven",
        Description = "Sheltered desert oasis for camels and reptiles.",
        Size = 780,
        securityLevel = SecurityLevel.Medium,
        Habitat = Enclosure.HabitatType.Desert,
        Climate = Enclosure.ClimateType.Temperate
    },
    new Enclosure
    {
        Id = 110,
        Name = "Temperate Trails",
        Description = "Mild climate zone for deer and small mammals.",
        Size = 850,
        securityLevel = SecurityLevel.Low,
        Habitat = Enclosure.HabitatType.Grassland,
        Climate = Enclosure.ClimateType.Temperate
    }
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
