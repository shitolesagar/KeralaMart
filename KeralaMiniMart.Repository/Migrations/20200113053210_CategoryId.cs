using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class CategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Notification",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Notification_CategoryId",
                table: "Notification",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notification_Category_CategoryId",
                table: "Notification",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notification_Category_CategoryId",
                table: "Notification");

            migrationBuilder.DropIndex(
                name: "IX_Notification_CategoryId",
                table: "Notification");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Notification");
        }
    }
}
