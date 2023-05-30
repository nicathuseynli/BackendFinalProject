using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class productpage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HomeProducts_HomeCategories_HomeCategoryId",
                table: "HomeProducts");

            migrationBuilder.DropTable(
                name: "HomeCategories");

            migrationBuilder.DropIndex(
                name: "IX_HomeProducts_HomeCategoryId",
                table: "HomeProducts");

            migrationBuilder.DropColumn(
                name: "HomeCategoryId",
                table: "HomeProducts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HomeCategoryId",
                table: "HomeProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "HomeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BestSeller = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Featured = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latest = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeProducts_HomeCategoryId",
                table: "HomeProducts",
                column: "HomeCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_HomeProducts_HomeCategories_HomeCategoryId",
                table: "HomeProducts",
                column: "HomeCategoryId",
                principalTable: "HomeCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
