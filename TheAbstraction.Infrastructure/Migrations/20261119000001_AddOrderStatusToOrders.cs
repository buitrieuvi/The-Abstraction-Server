using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using TheAbstraction.Infrastructure.Data;

namespace TheAbstraction.Infrastructure.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20261119000001_AddOrderStatusToOrders")]
public sealed class AddOrderStatusToOrders : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "OrderStatus",
            table: "Orders",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: "Pending");
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "OrderStatus", table: "Orders");
    }
}
