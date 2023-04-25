using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class ColorsIdAllowNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Color_ColorsId",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Size_SizeId",
                table: "CartDetail");

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "CartDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "ColorsId",
                table: "CartDetail",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Color_ColorsId",
                table: "CartDetail",
                column: "ColorsId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Size_SizeId",
                table: "CartDetail",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Color_ColorsId",
                table: "CartDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetail_Size_SizeId",
                table: "CartDetail");

            migrationBuilder.AlterColumn<int>(
                name: "SizeId",
                table: "CartDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ColorsId",
                table: "CartDetail",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Color_ColorsId",
                table: "CartDetail",
                column: "ColorsId",
                principalTable: "Color",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetail_Size_SizeId",
                table: "CartDetail",
                column: "SizeId",
                principalTable: "Size",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
