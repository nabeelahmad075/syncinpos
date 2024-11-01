using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace syncinpos.Migrations
{
    /// <inheritdoc />
    public partial class ItemDefinitionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "tblSections",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "tblUnitOfMeasurements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tblUnitOfMeasurements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tblItemDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(type: "int", nullable: false),
                    ItemTypeId = table.Column<int>(type: "int", nullable: false),
                    ItemCategoryId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true),
                    Barcode = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: true),
                    UOMId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_tblItemDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tblItemDefinitions_tblItemCategories_ItemCategoryId",
                        column: x => x.ItemCategoryId,
                        principalTable: "tblItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblItemDefinitions_tblItemTypes_ItemTypeId",
                        column: x => x.ItemTypeId,
                        principalTable: "tblItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblItemDefinitions_tblSections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "tblSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tblItemDefinitions_tblUnitOfMeasurements_UOMId",
                        column: x => x.UOMId,
                        principalTable: "tblUnitOfMeasurements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tblItemDefinitions_ItemCategoryId",
                table: "tblItemDefinitions",
                column: "ItemCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemDefinitions_ItemTypeId",
                table: "tblItemDefinitions",
                column: "ItemTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemDefinitions_SectionId",
                table: "tblItemDefinitions",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_tblItemDefinitions_UOMId",
                table: "tblItemDefinitions",
                column: "UOMId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tblItemDefinitions");

            migrationBuilder.DropTable(
                name: "tblUnitOfMeasurements");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "tblSections");
        }
    }
}
