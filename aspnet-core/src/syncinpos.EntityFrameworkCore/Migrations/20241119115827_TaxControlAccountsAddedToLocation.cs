using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class TaxControlAccountsAddedToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BankTaxDetailAccountId",
                table: "tblLocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CashTaxDetailAccountId",
                table: "tblLocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditCardTaxDetailAccountId",
                table: "tblLocations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreditTaxDetailAccountId",
                table: "tblLocations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblLocations_BankTaxDetailAccountId",
                table: "tblLocations",
                column: "BankTaxDetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLocations_CashTaxDetailAccountId",
                table: "tblLocations",
                column: "CashTaxDetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLocations_CreditCardTaxDetailAccountId",
                table: "tblLocations",
                column: "CreditCardTaxDetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tblLocations_CreditTaxDetailAccountId",
                table: "tblLocations",
                column: "CreditTaxDetailAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_BankTaxDetailAccountId",
                table: "tblLocations",
                column: "BankTaxDetailAccountId",
                principalTable: "tblDetailAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_CashTaxDetailAccountId",
                table: "tblLocations",
                column: "CashTaxDetailAccountId",
                principalTable: "tblDetailAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_CreditCardTaxDetailAccountId",
                table: "tblLocations",
                column: "CreditCardTaxDetailAccountId",
                principalTable: "tblDetailAccounts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_CreditTaxDetailAccountId",
                table: "tblLocations",
                column: "CreditTaxDetailAccountId",
                principalTable: "tblDetailAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_BankTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_CashTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_CreditCardTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_tblLocations_tblDetailAccounts_CreditTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropIndex(
                name: "IX_tblLocations_BankTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropIndex(
                name: "IX_tblLocations_CashTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropIndex(
                name: "IX_tblLocations_CreditCardTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropIndex(
                name: "IX_tblLocations_CreditTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "BankTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "CashTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "CreditCardTaxDetailAccountId",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "CreditTaxDetailAccountId",
                table: "tblLocations");
        }
    }
}
