using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class NotificationTableUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryLocationId",
                table: "Notification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_DeliveryLocationId",
                table: "Notification",
                column: "DeliveryLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_DeliveryLocation_DeliveryLocationId",
                table: "Notification",
                column: "DeliveryLocationId",
                principalTable: "DeliveryLocation",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_DeliveryLocation_DeliveryLocationId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_DeliveryLocationId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "DeliveryLocationId",
                table: "Notification");
        }
    }
}
