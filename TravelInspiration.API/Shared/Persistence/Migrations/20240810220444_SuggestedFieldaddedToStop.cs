using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TravelInspiration.API.Shared.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class SuggestedFieldaddedToStop : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Suggested",
                table: "Stops",
                type: "bit",
                nullable: true,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2047));

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2050));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2316));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2328));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2337));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2345));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2353));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 10, 22, 4, 44, 333, DateTimeKind.Utc).AddTicks(2361));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Suggested",
                table: "Stops");

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 773, DateTimeKind.Utc).AddTicks(9987));

            migrationBuilder.UpdateData(
                table: "Itineraries",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 773, DateTimeKind.Utc).AddTicks(9991));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(261));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(271));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 3,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(278));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 4,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(285));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 5,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(292));

            migrationBuilder.UpdateData(
                table: "Stops",
                keyColumn: "Id",
                keyValue: 6,
                column: "CreatedOn",
                value: new DateTime(2024, 8, 8, 20, 0, 30, 774, DateTimeKind.Utc).AddTicks(299));
        }
    }
}
