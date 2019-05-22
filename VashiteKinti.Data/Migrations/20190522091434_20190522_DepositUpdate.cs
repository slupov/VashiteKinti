using Microsoft.EntityFrameworkCore.Migrations;

namespace VashiteKinti.Data.Migrations
{
    public partial class _20190522_DepositUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreditOpportunity",
                table: "Deposits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExtraMoneyPayIn",
                table: "Deposits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Holder",
                table: "Deposits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InterestType",
                table: "Deposits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OverdraftOpportunity",
                table: "Deposits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Period",
                table: "Deposits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Size",
                table: "Deposits",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreditOpportunity",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "ExtraMoneyPayIn",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Holder",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "InterestType",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "OverdraftOpportunity",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Period",
                table: "Deposits");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Deposits");
        }
    }
}
