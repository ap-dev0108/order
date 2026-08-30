using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EditingMenuItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuCategories_MenuItems_MenuItemId",
                table: "MenuCategories");

            migrationBuilder.DropIndex(
                name: "IX_MenuCategories_MenuItemId",
                table: "MenuCategories");

            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "MenuCategories");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "MenuItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItems_MenuCategories_CategoryId",
                table: "MenuItems",
                column: "CategoryId",
                principalTable: "MenuCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItems_MenuCategories_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropIndex(
                name: "IX_MenuItems_CategoryId",
                table: "MenuItems");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "MenuItems");

            migrationBuilder.AddColumn<Guid>(
                name: "MenuItemId",
                table: "MenuCategories",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MenuCategories_MenuItemId",
                table: "MenuCategories",
                column: "MenuItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuCategories_MenuItems_MenuItemId",
                table: "MenuCategories",
                column: "MenuItemId",
                principalTable: "MenuItems",
                principalColumn: "Id");
        }
    }
}
