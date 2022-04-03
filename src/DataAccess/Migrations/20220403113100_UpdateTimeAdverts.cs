using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class UpdateTimeAdverts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "CreatedAt", table: "Adverts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
