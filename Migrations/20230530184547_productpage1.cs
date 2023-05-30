using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class productpage1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HoverImage",
                table: "HomeProducts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HoverImage",
                table: "HomeProducts");
        }
    }
}
