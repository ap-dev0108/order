using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatingtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions");

            migrationBuilder.DropColumn(
                name: "DinningId",
                table: "RestaurantTables");

            migrationBuilder.DropColumn(
                name: "EndedAt",
                table: "DinningSessions");

            migrationBuilder.CreateIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions",
                column: "TableId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions");

            migrationBuilder.AddColumn<Guid>(
                name: "DinningId",
                table: "RestaurantTables",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndedAt",
                table: "DinningSessions",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions",
                column: "TableId",
                unique: true);
        }
    }
}
