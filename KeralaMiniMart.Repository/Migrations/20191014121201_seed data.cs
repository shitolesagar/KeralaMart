using KeralaMiniMart.Entities.Constant;
using Microsoft.EntityFrameworkCore.Migrations;

namespace KeralaMiniMart.Repository.Migrations
{
    public partial class seeddata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Roles
            migrationBuilder.Sql("INSERT INTO `" + EnvironmentConstants.DatabaseName + "`.`Role` (`Name`) VALUES ('Admin');");
            migrationBuilder.Sql("INSERT INTO `" + EnvironmentConstants.DatabaseName + "`.`Role` (`Name`) VALUES ('Customer');");

            // SmtpMail
            migrationBuilder.Sql("INSERT INTO `" + EnvironmentConstants.DatabaseName + "`.`SmtpMail` (`FromMail`, `SmtpPassword`, `Host`, `Port`, `DisplayName`, `Description`) VALUES ('keralamartuser@gmail.com', 'Kerala1234@', 'smtp.gmail.com', '587', 'Kerala Mini Mart', 'Kerla mini mart ');");

            // AppTheme

            //
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
