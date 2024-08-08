using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TravelInspiration.API.Shared.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Itineraries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2500)", maxLength: 2500, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Itineraries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stops",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImageUri = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItineraryId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Stops_Itineraries_ItineraryId",
                        column: x => x.ItineraryId,
                        principalTable: "Itineraries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Itineraries",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "Description", "LastModified", "LastModifiedBy", "Name", "UserId" },
                values: new object[,]
                {
                    { 1, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 773, DateTimeKind.Utc).AddTicks(9987), "Five great days in Paris", null, "", "A Trip to Paris", "dummyuserid" },
                    { 2, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 773, DateTimeKind.Utc).AddTicks(9991), "A week in beautiful Antwerp", null, "", "Antwerp Extravaganza", "dummyuserid" }
                });

            migrationBuilder.InsertData(
                table: "Stops",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "ImageUri", "ItineraryId", "LastModified", "LastModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(261), "https://localhost:7120/images/eiffeltower.jpg", 1, null, "", "The Eiffel Tower" },
                    { 2, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(271), "https://localhost:7120/images/louvre.jpg", 1, null, "", "The Louvre" },
                    { 3, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(278), "https://localhost:7120/images/perelachaise.jpg", 1, null, "", "Père Lachaise Cemetery" },
                    { 4, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(285), "https://localhost:7120/images/royalmuseum.jpg", 2, null, "", "The Royal Museum of Beautiful Arts" },
                    { 5, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(292), "https://localhost:7120/images/stpauls.jpg", 2, null, "", "Saint Paul's Church" },
                    { 6, "DATASEED", new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(299), "https://localhost:7120/images/michelin.jpg", 2, null, "", "Michelin Restaurant Visit" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stops_ItineraryId",
                table: "Stops",
                column: "ItineraryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stops");

            migrationBuilder.DropTable(
                name: "Itineraries");
        }
    }
}
