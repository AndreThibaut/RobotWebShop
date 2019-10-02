using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotWebShop.Migrations
{
    public partial class OrderStock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderRobot");

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StripeRef",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "email",
                table: "Orders",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "OrderStocks",
                columns: table => new
                {
                    OrderID = table.Column<int>(nullable: false),
                    StockID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStocks", x => new { x.StockID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_OrderStocks_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderStocks_Stock_StockID",
                        column: x => x.StockID,
                        principalTable: "Stock",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStocks_OrderID",
                table: "OrderStocks",
                column: "OrderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStocks");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "StripeRef",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "email",
                table: "Orders");

            migrationBuilder.CreateTable(
                name: "OrderRobot",
                columns: table => new
                {
                    RobotID = table.Column<int>(nullable: false),
                    OrderID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderRobot", x => new { x.RobotID, x.OrderID });
                    table.ForeignKey(
                        name: "FK_OrderRobot_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderRobot_Robots_RobotID",
                        column: x => x.RobotID,
                        principalTable: "Robots",
                        principalColumn: "RobotID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderRobot_OrderID",
                table: "OrderRobot",
                column: "OrderID");
        }
    }
}
