using Microsoft.EntityFrameworkCore.Migrations;

namespace photographic_studio_api.Migrations
{
    public partial class addPublishStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsPublish",
                table: "UserImage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "UserImage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IsPublish",
                table: "OrderList",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublish",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "UserImage");

            migrationBuilder.DropColumn(
                name: "IsPublish",
                table: "OrderList");
        }
    }
}
