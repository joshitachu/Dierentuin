using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animals",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Animals",
                columns: new[] { "Id", "AnimalSize", "CategoryId", "Diet", "EnclosureId", "Name", "Prey", "SecurityRequirement", "SpaceRequirement", "Species", "Status", "activityPattern" },
                values: new object[] { 2, 0, 2, 0, null, "Papegaai", "Zaden", 0, 0.0, "Ara", null, 0 });
        }
    }
}
