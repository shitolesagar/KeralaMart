using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class deliverylocationforeignkeywithuseraddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryLocationId",
                table: "UserAddress",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_DeliveryLocationId",
                table: "UserAddress",
                column: "DeliveryLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAddress_DeliveryLocation_DeliveryLocationId",
                table: "UserAddress",
                column: "DeliveryLocationId",
                principalTable: "DeliveryLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAddress_DeliveryLocation_DeliveryLocationId",
                table: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_UserAddress_DeliveryLocationId",
                table: "UserAddress");

            migrationBuilder.DropColumn(
                name: "DeliveryLocationId",
                table: "UserAddress");
        }
    }
}
