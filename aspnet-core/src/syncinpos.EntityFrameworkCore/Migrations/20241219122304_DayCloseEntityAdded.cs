using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class DayCloseEntityAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblDayClose",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CurrentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblDayClose", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblDayClose_AbpUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblDayClose_AbpUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblDayClose_AbpUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_tblDayClose_tblLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "tblLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblDayClose_CreatorUserId",
                table: "tblDayClose",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblDayClose_DeleterUserId",
                table: "tblDayClose",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblDayClose_LastModifierUserId",
                table: "tblDayClose",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_tblDayClose_LocationId",
                table: "tblDayClose",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblDayClose");
        }
    }
}
