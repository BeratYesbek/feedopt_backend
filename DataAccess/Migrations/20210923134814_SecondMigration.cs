using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalSpecies_AnimalCategories_AnimalCategoryId",
                table: "AnimalSpecies");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingDeclarations_AnimalSpecies_AnimalSpeciesId",
                table: "MissingDeclarations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MissingDeclarations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalSpeciesId",
                table: "MissingDeclarations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "MissingDeclarations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Lan",
                table: "MissingDeclarations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Lon",
                table: "MissingDeclarations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "MissingDeclarations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalCategoryId",
                table: "AnimalSpecies",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AdoptionNotices",
                columns: table => new
                {
                    AdoptionNoticeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Lan = table.Column<long>(type: "bigint", nullable: false),
                    Lon = table.Column<long>(type: "bigint", nullable: false),
                    AnimalSpeciesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionNotices", x => x.AdoptionNoticeId);
                    table.ForeignKey(
                        name: "FK_AdoptionNotices_AnimalSpecies_AnimalSpeciesId",
                        column: x => x.AnimalSpeciesId,
                        principalTable: "AnimalSpecies",
                        principalColumn: "AnimalSpeciesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdoptionNotices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissingDeclarations_UserId",
                table: "MissingDeclarations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionNotices_AnimalSpeciesId",
                table: "AdoptionNotices",
                column: "AnimalSpeciesId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionNotices_UserId",
                table: "AdoptionNotices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalSpecies_AnimalCategories_AnimalCategoryId",
                table: "AnimalSpecies",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategories",
                principalColumn: "AnimalCategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingDeclarations_AnimalSpecies_AnimalSpeciesId",
                table: "MissingDeclarations",
                column: "AnimalSpeciesId",
                principalTable: "AnimalSpecies",
                principalColumn: "AnimalSpeciesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingDeclarations_Users_UserId",
                table: "MissingDeclarations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AnimalSpecies_AnimalCategories_AnimalCategoryId",
                table: "AnimalSpecies");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingDeclarations_AnimalSpecies_AnimalSpeciesId",
                table: "MissingDeclarations");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingDeclarations_Users_UserId",
                table: "MissingDeclarations");

            migrationBuilder.DropTable(
                name: "AdoptionNotices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_MissingDeclarations_UserId",
                table: "MissingDeclarations");

            migrationBuilder.DropColumn(
                name: "City",
                table: "MissingDeclarations");

            migrationBuilder.DropColumn(
                name: "Lan",
                table: "MissingDeclarations");

            migrationBuilder.DropColumn(
                name: "Lon",
                table: "MissingDeclarations");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "MissingDeclarations");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "MissingDeclarations",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AnimalSpeciesId",
                table: "MissingDeclarations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AnimalCategoryId",
                table: "AnimalSpecies",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AnimalSpecies_AnimalCategories_AnimalCategoryId",
                table: "AnimalSpecies",
                column: "AnimalCategoryId",
                principalTable: "AnimalCategories",
                principalColumn: "AnimalCategoryId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingDeclarations_AnimalSpecies_AnimalSpeciesId",
                table: "MissingDeclarations",
                column: "AnimalSpeciesId",
                principalTable: "AnimalSpecies",
                principalColumn: "AnimalSpeciesId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
