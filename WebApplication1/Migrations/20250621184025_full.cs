using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class full : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Zoos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zoos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enclosures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<double>(type: "float", nullable: false),
                    securityLevel = table.Column<int>(type: "int", nullable: false),
                    Habitat = table.Column<int>(type: "int", nullable: false),
                    Climate = table.Column<int>(type: "int", nullable: false),
                    ZooId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enclosures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Enclosures_Zoos_ZooId",
                        column: x => x.ZooId,
                        principalTable: "Zoos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Species = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    AnimalSize = table.Column<int>(type: "int", nullable: false),
                    Diet = table.Column<int>(type: "int", nullable: false),
                    activityPattern = table.Column<int>(type: "int", nullable: false),
                    Prey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnclosureId = table.Column<int>(type: "int", nullable: true),
                    SpaceRequirement = table.Column<double>(type: "float", nullable: false),
                    SecurityRequirement = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Animals_Enclosures_EnclosureId",
                        column: x => x.EnclosureId,
                        principalTable: "Enclosures",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Zoogdieren" },
                    { 2, "Vogels" },
                    { 3, "Reptielen" },
                    { 4, "Amfibieën" },
                    { 5, "Vissen" },
                    { 6, "Insecten" },
                    { 7, "Schaaldieren" },
                    { 8, "Weekdieren" },
                    { 9, "Kringloopdieren" },
                    { 10, "Buideldieren" },
                    { 11, "Pinnipedia" },
                    { 12, "Carnivoren" },
                    { 13, "Herbivoren" },
                    { 14, "Omnivoren" },
                    { 15, "Primaten" },
                    { 16, "Carnivora" },
                    { 17, "Cetacea" },
                    { 18, "Chiroptera" },
                    { 19, "Rodentia" },
                    { 20, "Artiodactyla" }
                });

            migrationBuilder.InsertData(
                table: "Zoos",
                columns: new[] { "Id", "Location", "Name" },
                values: new object[,]
                {
                    { 1, "Amsterdam", "Dierentuin Amsterdam" },
                    { 2, "Rhenen", "Ouwehands Dierenpark" },
                    { 3, "Amsterdam", "Artis Royal Zoo" },
                    { 4, "Arnhem", "Burgers' Zoo" },
                    { 5, "Hilvarenbeek", "Safaripark Beekse Bergen" },
                    { 6, "Rotterdam", "Blijdorp" },
                    { 7, "Rotterdam", "Zoo Rotterdam" },
                    { 8, "Stuttgart", "Wilhelma" },
                    { 9, "Wenen", "Tiergarten Schönbrunn" },
                    { 10, "Londen", "London Zoo" },
                    { 11, "Berlijn", "Berlin Zoologischer Garten" },
                    { 12, "Praag", "Prague Zoo" },
                    { 13, "France", "ZooParc de Beauval" },
                    { 14, "San Diego", "San Diego Zoo" },
                    { 15, "New York", "Bronx Zoo" },
                    { 16, "Sydney", "Taronga Zoo" },
                    { 17, "Singapore", "Singapore Zoo" },
                    { 18, "Kopenhagen", "Copenhagen Zoo" },
                    { 19, "Beijing", "Beijing Zoo" },
                    { 20, "Edinburgh", "Edinburgh Zoo" }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "AnimalSize", "CategoryId", "Diet", "EnclosureId", "Name", "Prey", "SecurityRequirement", "SpaceRequirement", "Species", "Status", "activityPattern" },
                values: new object[,]
                {
                    { 1, 0, 12, 0, null, "Leeuw", "Gazelle", 0, 0.0, "Panthera leo", null, 0 },
                    { 2, 0, 13, 0, null, "Olifant", "Planten", 0, 0.0, "Loxodonta africana", null, 0 },
                    { 3, 0, 2, 0, null, "Papegaai", "Zaden", 0, 0.0, "Ara ararauna", null, 0 },
                    { 4, 0, 3, 0, null, "Boa", "Knaagdieren", 0, 0.0, "Boa constrictor", null, 0 },
                    { 5, 0, 4, 0, null, "Kikker", "Insecten", 0, 0.0, "Rana temporaria", null, 0 },
                    { 6, 0, 5, 0, null, "Goudvis", "Algen", 0, 0.0, "Carassius auratus", null, 0 },
                    { 7, 0, 6, 0, null, "Vlinder", "Nectar", 0, 0.0, "Danaus plexippus", null, 0 },
                    { 8, 0, 7, 0, null, "Kreeft", "Detritus", 0, 0.0, "Homarus gammarus", null, 0 },
                    { 9, 0, 8, 0, null, "Inktvis", "Visjes", 0, 0.0, "Octopus vulgaris", null, 0 },
                    { 10, 0, 2, 0, null, "Oehoe", "Muizen", 0, 0.0, "Bubo bubo", null, 0 },
                    { 11, 0, 2, 0, null, "Pinguïn", "Vis", 0, 0.0, "Aptenodytes forsteri", null, 0 },
                    { 12, 0, 13, 0, null, "Giraffe", "Bladeren", 0, 0.0, "Giraffa camelopardalis", null, 0 },
                    { 13, 0, 15, 0, null, "Chimpansee", "Fruit", 0, 0.0, "Pan troglodytes", null, 0 },
                    { 14, 0, 17, 0, null, "Walvis", "Krill", 0, 0.0, "Balaenoptera musculus", null, 0 },
                    { 15, 0, 18, 0, null, "Vleermuis", "Insecten", 0, 0.0, "Myotis myotis", null, 0 },
                    { 16, 0, 19, 0, null, "Muis", "Zaden", 0, 0.0, "Mus musculus", null, 0 },
                    { 17, 0, 20, 0, null, "Hert", "Planten", 0, 0.0, "Cervus elaphus", null, 0 },
                    { 18, 0, 20, 0, null, "Koe", "Gras", 0, 0.0, "Bos taurus", null, 0 },
                    { 19, 0, 12, 0, null, "Wolf", "Herten", 0, 0.0, "Canis lupus", null, 0 },
                    { 20, 0, 2, 0, null, "Eend", "Kleine vis", 0, 0.0, "Anas platyrhynchos", null, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CategoryId",
                table: "Animals",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals",
                column: "EnclosureId");

            migrationBuilder.CreateIndex(
                name: "IX_Enclosures_ZooId",
                table: "Enclosures",
                column: "ZooId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Enclosures");

            migrationBuilder.DropTable(
                name: "Zoos");
        }
    }
}
