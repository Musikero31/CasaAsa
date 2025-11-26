using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaAsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddOtherTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Addresses_AddressId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                table: "OrderDetail");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderListSummary_Order_OrderId",
                table: "OrderListSummary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderListSummary",
                table: "OrderListSummary");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                table: "Order");

            migrationBuilder.RenameTable(
                name: "OrderListSummary",
                newName: "OrderListSummaries");

            migrationBuilder.RenameTable(
                name: "OrderDetail",
                newName: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "Order",
                newName: "Orders");

            migrationBuilder.RenameIndex(
                name: "IX_OrderListSummary_OrderId",
                table: "OrderListSummaries",
                newName: "IX_OrderListSummaries_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetail_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_Order_AddressId",
                table: "Orders",
                newName: "IX_Orders_AddressId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderListSummaries",
                table: "OrderListSummaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MenuCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenuDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Photo = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    MenuCategoryId = table.Column<int>(type: "int", nullable: false),
                    ActiveStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuDetails_MenuCategories_MenuCategoryId",
                        column: x => x.MenuCategoryId,
                        principalTable: "MenuCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MenuDetails_MenuCategoryId",
                table: "MenuDetails",
                column: "MenuCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderListSummaries_Orders_OrderId",
                table: "OrderListSummaries",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderListSummaries_Orders_OrderId",
                table: "OrderListSummaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Addresses_AddressId",
                table: "Orders");

            migrationBuilder.DropTable(
                name: "MenuDetails");

            migrationBuilder.DropTable(
                name: "MenuCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderListSummaries",
                table: "OrderListSummaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Order");

            migrationBuilder.RenameTable(
                name: "OrderListSummaries",
                newName: "OrderListSummary");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OrderDetail");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_AddressId",
                table: "Order",
                newName: "IX_Order_AddressId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderListSummaries_OrderId",
                table: "OrderListSummary",
                newName: "IX_OrderListSummary_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetail",
                newName: "IX_OrderDetail_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                table: "Order",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderListSummary",
                table: "OrderListSummary",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetail",
                table: "OrderDetail",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Addresses_AddressId",
                table: "Order",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetail_Order_OrderId",
                table: "OrderDetail",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderListSummary_Order_OrderId",
                table: "OrderListSummary",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
