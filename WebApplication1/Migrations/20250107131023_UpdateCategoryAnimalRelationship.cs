using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCategoryAnimalRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_enclosureId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CategoryId",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "enclosureId",
                table: "Animals",
                newName: "EnclosureId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_enclosureId",
                table: "Animals",
                newName: "IX_Animals_EnclosureId");

            migrationBuilder.AlterColumn<int>(
                name: "EnclosureId",
                table: "Animals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CategoryId",
                table: "Animals",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals",
                column: "EnclosureId",
                principalTable: "Enclosures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Enclosures_EnclosureId",
                table: "Animals");

            migrationBuilder.DropIndex(
                name: "IX_Animals_CategoryId",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "EnclosureId",
                table: "Animals",
                newName: "enclosureId");

            migrationBuilder.RenameIndex(
                name: "IX_Animals_EnclosureId",
                table: "Animals",
                newName: "IX_Animals_enclosureId");

            migrationBuilder.AlterColumn<int>(
                name: "enclosureId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Animals_CategoryId",
                table: "Animals",
                column: "CategoryId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Enclosures_enclosureId",
                table: "Animals",
                column: "enclosureId",
                principalTable: "Enclosures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
