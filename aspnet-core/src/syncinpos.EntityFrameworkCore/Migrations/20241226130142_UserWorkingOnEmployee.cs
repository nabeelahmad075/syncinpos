using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class UserWorkingOnEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUser",
                table: "tblEmployees",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "tblEmployees",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tblEmployees_UserId",
                table: "tblEmployees",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_tblEmployees_AbpUsers_UserId",
                table: "tblEmployees",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tblEmployees_AbpUsers_UserId",
                table: "tblEmployees");

            migrationBuilder.DropIndex(
                name: "IX_tblEmployees_UserId",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "IsUser",
                table: "tblEmployees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "tblEmployees");
        }
    }
}
