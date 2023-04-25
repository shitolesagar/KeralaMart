using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class ProducttablechangedaccordingtoKMM : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Quantity",
                table: "Product",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnitId",
                table: "Product",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_UnitId",
                table: "Product",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Unit_UnitId",
                table: "Product",
                column: "UnitId",
                principalTable: "Unit",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Unit_UnitId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_UnitId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "Product");
        }
    }
}
