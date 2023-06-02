using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class Backendprojectt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopPageColour_ShopPageColour_ShopPageColourId",
                table: "ShopPageColour");

            migrationBuilder.DropIndex(
                name: "IX_ShopPageColour_ShopPageColourId",
                table: "ShopPageColour");

            migrationBuilder.DropColumn(
                name: "ShopPageColourId",
                table: "ShopPageColour");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopPageColourId",
                table: "ShopPageColour",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShopPageColour_ShopPageColourId",
                table: "ShopPageColour",
                column: "ShopPageColourId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopPageColour_ShopPageColour_ShopPageColourId",
                table: "ShopPageColour",
                column: "ShopPageColourId",
                principalTable: "ShopPageColour",
                principalColumn: "Id");
        }
    }
}
