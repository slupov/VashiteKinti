using Microsoft.EntityFrameworkCore.Migrations;

namespace VashiteKinti.Data.Migrations
{
    public partial class DepositCompositeUniqueKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits");

            migrationBuilder.DropIndex(
                name: "IX_Deposits_Name",
                table: "Deposits");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Deposits",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_BankId",
                table: "Deposits",
                column: "BankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits");

            migrationBuilder.DropIndex(
                name: "IX_Deposits_BankId",
                table: "Deposits");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Deposits",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Deposits",
                table: "Deposits",
                columns: new[] { "BankId", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Deposits_Name",
                table: "Deposits",
                column: "Name",
                unique: true);
        }
    }
}
