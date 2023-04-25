using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class notificationtableshowwebflagadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsShowOnWeb",
                table: "Notification",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsShowOnWeb",
                table: "Notification");
        }
    }
}
