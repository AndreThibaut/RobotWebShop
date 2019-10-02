using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RobotWebShop.Migrations
{
    public partial class StockOnHold : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StockOnHolds",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    StockID = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    ExpiryDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockOnHolds", x => x.ID);
                    table.ForeignKey(
                        name: "FK_StockOnHolds_Stock_StockID",
                        column: x => x.StockID,
                        principalTable: "Stock",
                        principalColumn: "StockID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockOnHolds_StockID",
                table: "StockOnHolds",
                column: "StockID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StockOnHolds");
        }
    }
}
