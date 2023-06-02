using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class backendsonproject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProducts_ShopPageColour_ShopPageColourId",
                table: "HomeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopPageColour",
                table: "ShopPageColour");

            migrationBuilder.RenameTable(
                name: "ShopPageColour",
                newName: "ShopPageColours");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopPageColours",
                table: "ShopPageColours",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProducts_ShopPageColours_ShopPageColourId",
                table: "HomeProducts",
                column: "ShopPageColourId",
                principalTable: "ShopPageColours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProducts_ShopPageColours_ShopPageColourId",
                table: "HomeProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShopPageColours",
                table: "ShopPageColours");

            migrationBuilder.RenameTable(
                name: "ShopPageColours",
                newName: "ShopPageColour");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShopPageColour",
                table: "ShopPageColour",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProducts_ShopPageColour_ShopPageColourId",
                table: "HomeProducts",
                column: "ShopPageColourId",
                principalTable: "ShopPageColour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
