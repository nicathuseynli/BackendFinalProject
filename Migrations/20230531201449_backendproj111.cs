﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend_Final_Project.Migrations
{
    public partial class backendproj111 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SingleProductDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleProductDescription", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SingleProductPageInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShippingInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutReturnInfo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuaranteeInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleProductPageInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SingleProductReviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    UserFullname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserComment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserImage = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingleProductReviews", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SingleProductDescription");

            migrationBuilder.DropTable(
                name: "SingleProductPageInformation");

            migrationBuilder.DropTable(
                name: "SingleProductReviews");
        }
    }
}