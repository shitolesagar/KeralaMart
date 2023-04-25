using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class Bannertablemodified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Banner",
                newName: "ModifiedDateTime");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDateTime",
                table: "Banner",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDateTime",
                table: "Banner");

            migrationBuilder.RenameColumn(
                name: "ModifiedDateTime",
                table: "Banner",
                newName: "CreatedDate");
        }
    }
}
