using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class NotificationDeliveryLocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationDeliveryLocations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    NotificationId = table.Column<int>(nullable: true),
                    DeliveryLocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationDeliveryLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationDeliveryLocations_DeliveryLocation_DeliveryLocat~",
                        column: x => x.DeliveryLocationId,
                        principalTable: "DeliveryLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_NotificationDeliveryLocations_Notification_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notification",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDeliveryLocations_DeliveryLocationId",
                table: "NotificationDeliveryLocations",
                column: "DeliveryLocationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationDeliveryLocations_NotificationId",
                table: "NotificationDeliveryLocations",
                column: "NotificationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationDeliveryLocations");
        }
    }
}
