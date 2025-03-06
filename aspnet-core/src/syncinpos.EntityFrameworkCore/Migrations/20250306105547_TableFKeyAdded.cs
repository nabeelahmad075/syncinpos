using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class TableFKeyAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_tblPOSMaster_TableId",
                table: "tblPOSMaster",
                column: "TableId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblPOSMaster_tblTables_TableId",
                table: "tblPOSMaster",
                column: "TableId",
                principalTable: "tblTables",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblPOSMaster_tblTables_TableId",
                table: "tblPOSMaster");

            migrationBuilder.DropIndex(
                name: "IX_tblPOSMaster_TableId",
                table: "tblPOSMaster");
        }
    }
}
