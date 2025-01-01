using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class DetailAccountIdToCustomer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DetailAccountId",
                table: "tblCustomers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblCustomers_DetailAccountId",
                table: "tblCustomers",
                column: "DetailAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblCustomers_tblDetailAccounts_DetailAccountId",
                table: "tblCustomers",
                column: "DetailAccountId",
                principalTable: "tblDetailAccounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblCustomers_tblDetailAccounts_DetailAccountId",
                table: "tblCustomers");

            migrationBuilder.DropIndex(
                name: "IX_tblCustomers_DetailAccountId",
                table: "tblCustomers");

            migrationBuilder.DropColumn(
                name: "DetailAccountId",
                table: "tblCustomers");
        }
    }
}
