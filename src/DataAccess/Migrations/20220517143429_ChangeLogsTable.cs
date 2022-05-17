using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class ChangeLogsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "log_date",
                table: "Logs",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "logDetail",
                table: "Logs",
                newName: "Thread");

            migrationBuilder.RenameColumn(
                name: "audit",
                table: "Logs",
                newName: "Message");

            migrationBuilder.AddColumn<string>(
                name: "Exception",
                table: "Logs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Logs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Logger",
                table: "Logs",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exception",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Logger",
                table: "Logs");

            migrationBuilder.RenameColumn(
                name: "Thread",
                table: "Logs",
                newName: "logDetail");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Logs",
                newName: "audit");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Logs",
                newName: "log_date");
        }
    }
}
