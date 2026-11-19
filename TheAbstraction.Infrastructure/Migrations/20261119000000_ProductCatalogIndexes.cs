using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20261119000000_ProductCatalogIndexes")]
public sealed class ProductCatalogIndexes : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateIndex(
            name: "IX_Products_Name",
            table: "Products",
            column: "Name");

        migrationBuilder.CreateIndex(
            name: "IX_ProductVariants_ProductId",
            table: "ProductVariants",
            column: "ProductId");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropIndex(name: "IX_ProductVariants_ProductId", table: "ProductVariants");
        migrationBuilder.DropIndex(name: "IX_Products_Name", table: "Products");
    }
}
