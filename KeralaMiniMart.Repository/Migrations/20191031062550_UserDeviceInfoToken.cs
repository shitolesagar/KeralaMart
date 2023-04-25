using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class UserDeviceInfoToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationUserId",
                table: "UserDeviceInfo",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserDeviceInfo_ApplicationUserId",
                table: "UserDeviceInfo",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDeviceInfo_ApplicationUser_ApplicationUserId",
                table: "UserDeviceInfo",
                column: "ApplicationUserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDeviceInfo_ApplicationUser_ApplicationUserId",
                table: "UserDeviceInfo");

            migrationBuilder.DropIndex(
                name: "IX_UserDeviceInfo_ApplicationUserId",
                table: "UserDeviceInfo");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "UserDeviceInfo");
        }
    }
}
