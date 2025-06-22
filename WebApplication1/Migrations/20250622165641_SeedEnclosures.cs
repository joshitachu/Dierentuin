using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SeedEnclosures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Zoos",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.InsertData(
                table: "Enclosures",
                columns: new[] { "Id", "Climate", "Description", "Habitat", "Name", "Size", "ZooId", "securityLevel" },
                values: new object[,]
                {
                    { 1, 0, "Large grassland area for African animals.", 3, "Savannah Zone", 1200.5, null, 1 },
                    { 2, 2, "Cold environment for penguins.", 1, "Penguin Cove", 800.0, null, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
