using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class backend162 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SocialMediaName",
                table: "SocialMediaAdresses",
                newName: "TwitterLink");

            migrationBuilder.RenameColumn(
                name: "SocialMedia",
                table: "SocialMediaAdresses",
                newName: "PinterestLink");

            migrationBuilder.AddColumn<string>(
                name: "DribbbleLink",
                table: "SocialMediaAdresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FacebookLink",
                table: "SocialMediaAdresses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DribbbleLink",
                table: "SocialMediaAdresses");

            migrationBuilder.DropColumn(
                name: "FacebookLink",
                table: "SocialMediaAdresses");

            migrationBuilder.RenameColumn(
                name: "TwitterLink",
                table: "SocialMediaAdresses",
                newName: "SocialMediaName");

            migrationBuilder.RenameColumn(
                name: "PinterestLink",
                table: "SocialMediaAdresses",
                newName: "SocialMedia");
        }
    }
}
