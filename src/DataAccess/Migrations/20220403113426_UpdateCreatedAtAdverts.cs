using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdateCreatedAtAdverts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Adverts",
                nullable: false,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
