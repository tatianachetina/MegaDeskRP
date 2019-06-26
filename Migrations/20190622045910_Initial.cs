using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaDeskRP.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SurfaceMaterialId1",
                table: "Desk",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RushOrder",
                columns: table => new
                {
                    RushOrderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShippingType = table.Column<string>(maxLength: 250, nullable: false),
                    PriceLessThan1000 = table.Column<decimal>(nullable: false),
                    Price1000To2000 = table.Column<decimal>(nullable: false),
                    PriceGreater2000 = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RushOrder", x => x.RushOrderId);
                });

            migrationBuilder.CreateTable(
                name: "SurfaceMaterial",
                columns: table => new
                {
                    SurfaceMaterialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaterialType = table.Column<string>(nullable: true),
                    MaterialPrice = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurfaceMaterial", x => x.SurfaceMaterialId);
                });

            migrationBuilder.CreateTable(
                name: "DeskQuote",
                columns: table => new
                {
                    DeskQuoteId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeskId = table.Column<int>(nullable: false),
                    CustomerName = table.Column<string>(nullable: false),
                    RushOrderId = table.Column<int>(nullable: false),
                    QuotePrice = table.Column<decimal>(nullable: false),
                    QuoteDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeskQuote", x => x.DeskQuoteId);
                    table.ForeignKey(
                        name: "FK_DeskQuote_Desk_DeskId",
                        column: x => x.DeskId,
                        principalTable: "Desk",
                        principalColumn: "DeskId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeskQuote_RushOrder_RushOrderId",
                        column: x => x.RushOrderId,
                        principalTable: "RushOrder",
                        principalColumn: "RushOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Desk_SurfaceMaterialId1",
                table: "Desk",
                column: "SurfaceMaterialId1");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_DeskId",
                table: "DeskQuote",
                column: "DeskId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_RushOrderId",
                table: "DeskQuote",
                column: "RushOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Desk_SurfaceMaterial_SurfaceMaterialId1",
                table: "Desk",
                column: "SurfaceMaterialId1",
                principalTable: "SurfaceMaterial",
                principalColumn: "SurfaceMaterialId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Desk_SurfaceMaterial_SurfaceMaterialId1",
                table: "Desk");

            migrationBuilder.DropTable(
                name: "DeskQuote");

            migrationBuilder.DropTable(
                name: "SurfaceMaterial");

            migrationBuilder.DropTable(
                name: "RushOrder");

            migrationBuilder.DropIndex(
                name: "IX_Desk_SurfaceMaterialId1",
                table: "Desk");

            migrationBuilder.DropColumn(
                name: "SurfaceMaterialId1",
                table: "Desk");
        }
    }
}
