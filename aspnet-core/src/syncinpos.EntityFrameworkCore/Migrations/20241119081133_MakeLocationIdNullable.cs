using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class MakeLocationIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblMainAccounts_tblLocations_LocationId",
                table: "tblMainAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "tblMainAccounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_tblMainAccounts_tblLocations_LocationId",
                table: "tblMainAccounts",
                column: "LocationId",
                principalTable: "tblLocations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblMainAccounts_tblLocations_LocationId",
                table: "tblMainAccounts");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "tblMainAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_tblMainAccounts_tblLocations_LocationId",
                table: "tblMainAccounts",
                column: "LocationId",
                principalTable: "tblLocations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
