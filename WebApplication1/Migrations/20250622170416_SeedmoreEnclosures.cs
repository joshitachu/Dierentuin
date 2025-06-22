using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SeedmoreEnclosures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.InsertData(
                table: "Enclosures",
                columns: new[] { "Id", "Climate", "Description", "Habitat", "Name", "Size", "ZooId", "securityLevel" },
                values: new object[,]
                {
                    { 101, 0, "Large grassland area for African animals.", 3, "Savannah Zone", 1200.5, null, 1 },
                    { 102, 2, "Cold environment for penguins.", 1, "Penguin Cove", 800.0, null, 2 },
                    { 103, 0, "Hot and dry enclosure for desert species.", 2, "Desert Dunes", 600.0, null, 0 },
                    { 104, 0, "Dense forest area for tropical animals.", 0, "Rainforest Retreat", 950.0, null, 1 },
                    { 105, 2, "Chilly area for polar bears and arctic foxes.", 3, "Arctic Tundra", 1100.0, null, 2 },
                    { 106, 0, "Lush forest enclosure for monkeys and birds.", 0, "Jungle Jump", 720.0, null, 1 },
                    { 107, 1, "Open space for giraffes and zebras.", 3, "Giraffe Plains", 1300.0, null, 0 },
                    { 108, 0, "Watery habitat for crocodiles.", 1, "Crocodile Creek", 500.0, null, 2 },
                    { 109, 1, "Sheltered desert oasis for camels and reptiles.", 2, "Oasis Haven", 780.0, null, 1 },
                    { 110, 1, "Mild climate zone for deer and small mammals.", 3, "Temperate Trails", 850.0, null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 101);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 102);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 103);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 104);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 105);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 106);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 107);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 108);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 109);

            migrationBuilder.DeleteData(
                table: "Enclosures",
                keyColumn: "Id",
                keyValue: 110);

            migrationBuilder.InsertData(
                table: "Enclosures",
                columns: new[] { "Id", "Climate", "Description", "Habitat", "Name", "Size", "ZooId", "securityLevel" },
                values: new object[,]
                {
                    { 1, 0, "Large grassland area for African animals.", 3, "Savannah Zone", 1200.5, null, 1 },
                    { 2, 2, "Cold environment for penguins.", 1, "Penguin Cove", 800.0, null, 2 }
                });
        }
    }
}
