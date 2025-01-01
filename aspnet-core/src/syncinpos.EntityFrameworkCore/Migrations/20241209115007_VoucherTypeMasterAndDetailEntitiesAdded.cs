using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class VoucherTypeMasterAndDetailEntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblVoucherType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    Alias = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblVoucherType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblVoucherMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    VoucherNo = table.Column<string>(type: "nvarchar(16)", maxLength: 16, nullable: true),
                    VoucherDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoucherTypeId = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
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
                    table.PrimaryKey("PK_tblVoucherMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblVoucherMaster_tblLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "tblLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblVoucherMaster_tblVoucherType_VoucherTypeId",
                        column: x => x.VoucherTypeId,
                        principalTable: "tblVoucherType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblVoucherDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoucherMasterId = table.Column<long>(type: "bigint", nullable: false),
                    DetailAccountId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    DebitAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    CreditAmount = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
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
                    table.PrimaryKey("PK_tblVoucherDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblVoucherDetail_tblDetailAccounts_DetailAccountId",
                        column: x => x.DetailAccountId,
                        principalTable: "tblDetailAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblVoucherDetail_tblVoucherMaster_VoucherMasterId",
                        column: x => x.VoucherMasterId,
                        principalTable: "tblVoucherMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblVoucherDetail_DetailAccountId",
                table: "tblVoucherDetail",
                column: "DetailAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVoucherDetail_VoucherMasterId",
                table: "tblVoucherDetail",
                column: "VoucherMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVoucherMaster_LocationId",
                table: "tblVoucherMaster",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_tblVoucherMaster_VoucherTypeId",
                table: "tblVoucherMaster",
                column: "VoucherTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblVoucherDetail");

            migrationBuilder.DropTable(
                name: "tblVoucherMaster");

            migrationBuilder.DropTable(
                name: "tblVoucherType");
        }
    }
}
