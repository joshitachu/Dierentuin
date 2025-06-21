using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Zoogdieren" },
                    { 2, "Vogels" }
                });

            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "AnimalSize", "CategoryId", "Diet", "EnclosureId", "Name", "Prey", "SecurityRequirement", "SpaceRequirement", "Species", "Status", "activityPattern" },
                values: new object[,]
                {
                    { 1, 0, 1, 0, null, "Leeuw", "Gazelle", 0, 0.0, "Panthera leo", null, 0 },
                    { 2, 0, 2, 0, null, "Papegaai", "Zaden", 0, 0.0, "Ara", null, 0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
