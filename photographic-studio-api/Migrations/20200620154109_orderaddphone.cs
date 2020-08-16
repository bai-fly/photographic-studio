using Microsoft.EntityFrameworkCore.Migrations;

namespace photographic_studio_api.Migrations
{
    public partial class orderaddphone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "OrderList",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "OrderList");
        }
    }
}
