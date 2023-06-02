using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class BackendprojecT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ShopPageColourId",
                table: "HomeProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ShopPageColour",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Colour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopPageColourId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPageColour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPageColour_ShopPageColour_ShopPageColourId",
                        column: x => x.ShopPageColourId,
                        principalTable: "ShopPageColour",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeProducts_ShopPageColourId",
                table: "HomeProducts",
                column: "ShopPageColourId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPageColour_ShopPageColourId",
                table: "ShopPageColour",
                column: "ShopPageColourId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProducts_ShopPageColour_ShopPageColourId",
                table: "HomeProducts",
                column: "ShopPageColourId",
                principalTable: "ShopPageColour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProducts_ShopPageColour_ShopPageColourId",
                table: "HomeProducts");

            migrationBuilder.DropTable(
                name: "ShopPageColour");

            migrationBuilder.DropIndex(
                name: "IX_HomeProducts_ShopPageColourId",
                table: "HomeProducts");

            migrationBuilder.DropColumn(
                name: "ShopPageColourId",
                table: "HomeProducts");
        }
    }
}
