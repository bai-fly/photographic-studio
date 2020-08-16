using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace photographic_studio_api.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    Cover = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: false),
                    IsHot = table.Column<uint>(nullable: false),
                    PreviewCount = table.Column<int>(nullable: false),
                    LikeCount = table.Column<uint>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Link = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderNumber = table.Column<string>(nullable: true),
                    Price = table.Column<string>(nullable: true),
                    Deposit = table.Column<decimal>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Cameraman = table.Column<int>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PricingPackage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Cover = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Deposit = table.Column<decimal>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingPackage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SysConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysConfig", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Avatar = table.Column<string>(nullable: true),
                    Sex = table.Column<int>(nullable: false),
                    Point = table.Column<decimal>(nullable: false),
                    Score = table.Column<decimal>(nullable: false),
                    Role = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    LoginDate = table.Column<DateTime>(nullable: false),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WapInfo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    Desc = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WapInfo", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "News");

            migrationBuilder.DropTable(
                name: "OrderList");

            migrationBuilder.DropTable(
                name: "PricingPackage");

            migrationBuilder.DropTable(
                name: "SysConfig");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "WapInfo");
        }
    }
}
