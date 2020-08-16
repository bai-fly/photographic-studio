using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace photographic_studio_api.Migrations
{
    public partial class AddTableUserImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Area",
                table: "OrderList",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "OrderList",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsTel",
                table: "OrderList",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Province",
                table: "OrderList",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserImage",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Path = table.Column<string>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImage", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserImage");

            migrationBuilder.DropColumn(
                name: "Area",
                table: "OrderList");

            migrationBuilder.DropColumn(
                name: "City",
                table: "OrderList");

            migrationBuilder.DropColumn(
                name: "IsTel",
                table: "OrderList");

            migrationBuilder.DropColumn(
                name: "Province",
                table: "OrderList");
        }
    }
}
