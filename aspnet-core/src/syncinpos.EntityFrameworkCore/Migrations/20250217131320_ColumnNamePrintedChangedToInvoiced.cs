using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class ColumnNamePrintedChangedToInvoiced : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsPrinted",
                table: "tblPOSMaster",
                newName: "IsInvoiced");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsInvoiced",
                table: "tblPOSMaster",
                newName: "IsPrinted");
        }
    }
}
