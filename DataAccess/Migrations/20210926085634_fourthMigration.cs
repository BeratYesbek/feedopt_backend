using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class fourthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AnimalImageId",
                table: "MissingDeclarationImages",
                newName: "MissingDeclarationImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "MissingDeclarationImageId",
                table: "MissingDeclarationImages",
                newName: "AnimalImageId");
        }
    }
}
