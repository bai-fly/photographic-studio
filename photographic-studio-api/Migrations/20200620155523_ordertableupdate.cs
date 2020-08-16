using Microsoft.EntityFrameworkCore.Migrations;

namespace photographic_studio_api.Migrations
{
    public partial class ordertableupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PricingPackageId",
                table: "OrderList",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricingPackageId",
                table: "OrderList");
        }
    }
}
