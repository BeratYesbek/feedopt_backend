using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdateAdverts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Adverts",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: DateTime.UtcNow);

   
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Adverts");
        }
    }
}
