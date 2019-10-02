using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotWebShop.Migrations
{
    public partial class StockOnHoldCorrection : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SessionID",
                table: "StockOnHolds",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "StockOnHolds");
        }
    }
}
