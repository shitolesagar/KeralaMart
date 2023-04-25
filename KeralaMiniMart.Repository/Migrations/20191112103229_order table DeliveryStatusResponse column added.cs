using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class ordertableDeliveryStatusResponsecolumnadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeliveredSmsResponse",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveredSmsResponse",
                table: "Order");
        }
    }
}
