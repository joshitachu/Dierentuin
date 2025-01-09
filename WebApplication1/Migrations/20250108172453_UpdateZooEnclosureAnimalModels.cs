using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class UpdateZooEnclosureAnimalModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "species",
                table: "Animals",
                newName: "Species");

            migrationBuilder.RenameColumn(
                name: "prey",
                table: "Animals",
                newName: "Prey");

            migrationBuilder.AddColumn<int>(
                name: "Climate",
                table: "Enclosures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Habitat",
                table: "Enclosures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Size",
                table: "Enclosures",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "ZooId",
                table: "Enclosures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "securityLevel",
                table: "Enclosures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Animals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AnimalSize",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Diet",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecurityRequirement",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "SpaceRequirement",
                table: "Animals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "activityPattern",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Enclosures_ZooId",
                table: "Enclosures",
                column: "ZooId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Enclosures_Zoos_ZooId",
                table: "Enclosures",
                column: "ZooId",
                principalTable: "Zoos",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals");

            migrationBuilder.DropForeignKey(
                name: "FK_Enclosures_Zoos_ZooId",
                table: "Enclosures");

            migrationBuilder.DropIndex(
                name: "IX_Enclosures_ZooId",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "Climate",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "Habitat",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "ZooId",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "securityLevel",
                table: "Enclosures");

            migrationBuilder.DropColumn(
                name: "AnimalSize",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "Diet",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SecurityRequirement",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "SpaceRequirement",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "activityPattern",
                table: "Animals");

            migrationBuilder.RenameColumn(
                name: "Species",
                table: "Animals",
                newName: "species");

            migrationBuilder.RenameColumn(
                name: "Prey",
                table: "Animals",
                newName: "prey");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Animals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Categories_CategoryId",
                table: "Animals",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
