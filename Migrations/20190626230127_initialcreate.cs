using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MegaDeskRP.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    MaterialPrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SurfaceMaterial", x => x.SurfaceMaterialId);
                });

            migrationBuilder.CreateTable(
                name: "Desk",
                columns: table => new
                {
                    DeskId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Width = table.Column<int>(nullable: false),
                    Depth = table.Column<int>(nullable: false),
                    NumberOfDrawers = table.Column<int>(nullable: false),
                    SurfaceMaterialId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Desk", x => x.DeskId);
                    table.ForeignKey(
                        name: "FK_Desk_SurfaceMaterial_SurfaceMaterialId",
                        column: x => x.SurfaceMaterialId,
                        principalTable: "SurfaceMaterial",
                        principalColumn: "SurfaceMaterialId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "IX_Desk_SurfaceMaterialId",
                table: "Desk",
                column: "SurfaceMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_DeskId",
                table: "DeskQuote",
                column: "DeskId");

            migrationBuilder.CreateIndex(
                name: "IX_DeskQuote_RushOrderId",
                table: "DeskQuote",
                column: "RushOrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeskQuote");

            migrationBuilder.DropTable(
                name: "Desk");

            migrationBuilder.DropTable(
                name: "RushOrder");

            migrationBuilder.DropTable(
                name: "SurfaceMaterial");
        }
    }
}
