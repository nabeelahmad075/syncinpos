using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class TaxAndInvoiceDetailsAddedToLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankChargesLabel",
                table: "tblLocations",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "BankChargesPercent",
                table: "tblLocations",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryCharges",
                table: "tblLocations",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "EnableBankCharges",
                table: "tblLocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableDeliveryCharges",
                table: "tblLocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "EnableServicesCharges",
                table: "tblLocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FixedDeliveryCharges",
                table: "tblLocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "FixedServicesCharges",
                table: "tblLocations",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<decimal>(
                name: "ServiceCharges",
                table: "tblLocations",
                type: "decimal(10,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ServiceChargesLabel",
                table: "tblLocations",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SlipNotes",
                table: "tblLocations",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentOnBank",
                table: "tblLocations",
                type: "decimal(5,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentOnCash",
                table: "tblLocations",
                type: "decimal(5,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentOnCredit",
                table: "tblLocations",
                type: "decimal(5,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TaxPercentOnCreditCard",
                table: "tblLocations",
                type: "decimal(5,4)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TaxRegistrationNo",
                table: "tblLocations",
                type: "nvarchar(32)",
                maxLength: 32,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxTitle",
                table: "tblLocations",
                type: "nvarchar(16)",
                maxLength: 16,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankChargesLabel",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "BankChargesPercent",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "DeliveryCharges",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "EnableBankCharges",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "EnableDeliveryCharges",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "EnableServicesCharges",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "FixedDeliveryCharges",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "FixedServicesCharges",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "ServiceCharges",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "ServiceChargesLabel",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "SlipNotes",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "TaxPercentOnBank",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "TaxPercentOnCash",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "TaxPercentOnCredit",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "TaxPercentOnCreditCard",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "TaxRegistrationNo",
                table: "tblLocations");

            migrationBuilder.DropColumn(
                name: "TaxTitle",
                table: "tblLocations");
        }
    }
}
