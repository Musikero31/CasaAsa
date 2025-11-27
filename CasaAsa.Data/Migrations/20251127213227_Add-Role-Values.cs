using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CasaAsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8db644e0-6c88-41e5-8be6-f28f3c455447", null, "Customer", "Customer" },
                    { "b52d7e53-5cdc-4dd5-8e45-19fb77e9a1e0", null, "Administrator", "Administrator" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8db644e0-6c88-41e5-8be6-f28f3c455447");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b52d7e53-5cdc-4dd5-8e45-19fb77e9a1e0");
        }
    }
}
