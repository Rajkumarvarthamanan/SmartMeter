using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartMeterAPI.Migrations
{
    public partial class ColumnChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Accounts_AccountNumber",
                table: "MeterReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "MeterReadings",
                newName: "AccountId");

            migrationBuilder.RenameIndex(
                name: "IX_MeterReadings_AccountNumber",
                table: "MeterReadings",
                newName: "IX_MeterReadings_AccountId");

            migrationBuilder.RenameColumn(
                name: "AccountNumber",
                table: "Accounts",
                newName: "AccountId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Accounts_AccountId",
                table: "MeterReadings",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MeterReadings_Accounts_AccountId",
                table: "MeterReadings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "MeterReadings",
                newName: "AccountNumber");

            migrationBuilder.RenameIndex(
                name: "IX_MeterReadings_AccountId",
                table: "MeterReadings",
                newName: "IX_MeterReadings_AccountNumber");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "Accounts",
                newName: "AccountNumber");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeterReadings_Accounts_AccountNumber",
                table: "MeterReadings",
                column: "AccountNumber",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
