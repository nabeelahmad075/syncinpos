using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class ItemPriceListAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tblItemPriceList",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false),
                    EffectedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                    table.PrimaryKey("PK_tblItemPriceList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblItemPriceList_tblItemCategories_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "tblItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblItemPriceList_tblItemDefinitions_ItemId",
                        column: x => x.ItemId,
                        principalTable: "tblItemDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblItemPriceList_tblLocations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "tblLocations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblItemPriceList_ItemCategoryId",
                table: "tblItemPriceList",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemPriceList_ItemId",
                table: "tblItemPriceList",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemPriceList_LocationId",
                table: "tblItemPriceList",
                column: "LocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItemPriceList");
        }
    }
}
