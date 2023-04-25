using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class ordertabledeletebehaviourchanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_UserAddress_UserAddressId",
                table: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_UserAddress_UserAddressId",
                table: "Order",
                column: "UserAddressId",
                principalTable: "UserAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_UserAddress_UserAddressId",
                table: "Order");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_UserAddress_UserAddressId",
                table: "Order",
                column: "UserAddressId",
                principalTable: "UserAddress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
