using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class ApplicationUserIdInNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "Notification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_ApplicationUserId",
                table: "Notification",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_ApplicationUser_ApplicationUserId",
                table: "Notification",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_ApplicationUser_ApplicationUserId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_ApplicationUserId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Notification");
        }
    }
}
