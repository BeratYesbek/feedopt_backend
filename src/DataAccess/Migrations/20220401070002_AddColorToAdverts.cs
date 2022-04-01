using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class AddColorToAdverts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Color",
                table: "Adverts");

            migrationBuilder.AddColumn<int>(
                name: "ColorId",
                table: "Adverts",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ColorId",
                table: "Adverts");

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Adverts",
                type: "text",
                nullable: true);
        }
    }
}
