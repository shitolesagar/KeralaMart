using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class renameIsAvailabletoIsAutomateStockMaintainance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Product",
                newName: "IsAutomateStockMaintainance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAutomateStockMaintainance",
                table: "Product",
                newName: "IsAvailable");
        }
    }
}
