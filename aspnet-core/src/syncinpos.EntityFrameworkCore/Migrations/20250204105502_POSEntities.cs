using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class POSEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblPOSMaster",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    InvoiceNo = table.Column<long>(type: "bigint", nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    IsPrinted = table.Column<bool>(type: "bit", nullable: true),
                    PrintDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ServiceTypeId = table.Column<int>(type: "int", nullable: false),
                    PaymentMode = table.Column<int>(type: "int", nullable: true),
                    CoverTable = table.Column<int>(type: "int", nullable: true),
                    TableId = table.Column<int>(type: "int", nullable: true),
                    DeliveryChargesPer = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    DeliveryCharges = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    ServiceChargesPer = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    ServiceCharges = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    BankChargesPer = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    BankCharges = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    SalesTaxPer = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    SalesTaxAmount = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    DiscountPer = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    GrossAmount = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    NetAmount = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    PaymentIn = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Balance = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
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
                    table.PrimaryKey("PK_tblPOSMaster", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPOSMaster_tblCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "tblCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPOSMaster_tblEmployees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "tblEmployees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPOSMaster_tblLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "tblLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tblPOSDetail",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POSMasterId = table.Column<long>(type: "bigint", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(10,4)", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Cancelled = table.Column<bool>(type: "bit", nullable: true),
                    CancelledOn = table.Column<DateTime>(type: "datetime2", nullable: true),
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
                    table.PrimaryKey("PK_tblPOSDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblPOSDetail_tblItemDefinitions_ItemId",
                        column: x => x.ItemId,
                        principalTable: "tblItemDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblPOSDetail_tblPOSMaster_POSMasterId",
                        column: x => x.POSMasterId,
                        principalTable: "tblPOSMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblPOSDetail_ItemId",
                table: "tblPOSDetail",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPOSDetail_POSMasterId",
                table: "tblPOSDetail",
                column: "POSMasterId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPOSMaster_CustomerId",
                table: "tblPOSMaster",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPOSMaster_EmployeeId",
                table: "tblPOSMaster",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblPOSMaster_LocationId",
                table: "tblPOSMaster",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblPOSDetail");

            migrationBuilder.DropTable(
                name: "tblPOSMaster");
        }
    }
}
