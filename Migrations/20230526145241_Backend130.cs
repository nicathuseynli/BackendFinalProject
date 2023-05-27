using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class Backend130 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoverImage",
                table: "HomeProducts");

            migrationBuilder.RenameColumn(
                name: "MainImage",
                table: "HomeProducts",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "HomeCategories",
                newName: "Latest");

            migrationBuilder.AddColumn<string>(
                name: "BestSeller",
                table: "HomeCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Featured",
                table: "HomeCategories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BestSeller",
                table: "HomeCategories");

            migrationBuilder.DropColumn(
                name: "Featured",
                table: "HomeCategories");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "HomeProducts",
                newName: "MainImage");

            migrationBuilder.RenameColumn(
                name: "Latest",
                table: "HomeCategories",
                newName: "Type");

            migrationBuilder.AddColumn<string>(
                name: "HoverImage",
                table: "HomeProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
