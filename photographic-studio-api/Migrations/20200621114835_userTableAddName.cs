using Microsoft.EntityFrameworkCore.Migrations;

namespace photographic_studio_api.Migrations
{
    public partial class userTableAddName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "UserInfo",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "UserInfo");
        }
    }
}
