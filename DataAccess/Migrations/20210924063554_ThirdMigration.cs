using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "City",
                table: "AdoptionNotices");

            migrationBuilder.DropColumn(
                name: "Lan",
                table: "AdoptionNotices");

            migrationBuilder.DropColumn(
                name: "Lon",
                table: "AdoptionNotices");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "MissingDeclarations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "AdoptionNotices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "AdoptionNoticeImages",
                columns: table => new
                {
                    AdoptionNoticeImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdoptionNoticeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdoptionNoticeImages", x => x.AdoptionNoticeImageId);
                    table.ForeignKey(
                        name: "FK_AdoptionNoticeImages_AdoptionNotices_AdoptionNoticeId",
                        column: x => x.AdoptionNoticeId,
                        principalTable: "AdoptionNotices",
                        principalColumn: "AdoptionNoticeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PlaceId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Latitude = table.Column<long>(type: "bigint", nullable: false),
                    Longitude = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "MissingDeclarationImages",
                columns: table => new
                {
                    AnimalImageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MissingDeclarationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MissingDeclarationImages", x => x.AnimalImageId);
                    table.ForeignKey(
                        name: "FK_MissingDeclarationImages_MissingDeclarations_MissingDeclarationId",
                        column: x => x.MissingDeclarationId,
                        principalTable: "MissingDeclarations",
                        principalColumn: "MissingDeclarationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MissingDeclarations_LocationId",
                table: "MissingDeclarations",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionNotices_LocationId",
                table: "AdoptionNotices",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionNoticeImages_AdoptionNoticeId",
                table: "AdoptionNoticeImages",
                column: "AdoptionNoticeId");

            migrationBuilder.CreateIndex(
                name: "IX_MissingDeclarationImages_MissingDeclarationId",
                table: "MissingDeclarationImages",
                column: "MissingDeclarationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionNotices_Locations_LocationId",
                table: "AdoptionNotices",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MissingDeclarations_Locations_LocationId",
                table: "MissingDeclarations",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "LocationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionNotices_Locations_LocationId",
                table: "AdoptionNotices");

            migrationBuilder.DropForeignKey(
                name: "FK_MissingDeclarations_Locations_LocationId",
                table: "MissingDeclarations");

            migrationBuilder.DropTable(
                name: "AdoptionNoticeImages");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "MissingDeclarationImages");

            migrationBuilder.DropIndex(
                name: "IX_MissingDeclarations_LocationId",
                table: "MissingDeclarations");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionNotices_LocationId",
                table: "AdoptionNotices");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "MissingDeclarations");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "AdoptionNotices");

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

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AdoptionNotices",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "Lan",
                table: "AdoptionNotices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Lon",
                table: "AdoptionNotices",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
