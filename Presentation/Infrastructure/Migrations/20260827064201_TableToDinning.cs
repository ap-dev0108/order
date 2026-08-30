using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TableToDinning : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions");

            migrationBuilder.CreateIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions",
                column: "TableId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions");

            migrationBuilder.CreateIndex(
                name: "IX_DinningSessions_TableId",
                table: "DinningSessions",
                column: "TableId");
        }
    }
}
