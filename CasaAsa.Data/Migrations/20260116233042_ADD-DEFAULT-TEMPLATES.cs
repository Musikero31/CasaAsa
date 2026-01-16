using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CasaAsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADDDEFAULTTEMPLATES : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationSettings",
                columns: new[] { "Id", "ActiveStatus", "Category", "Code", "CreatedBy", "CreatedDate", "UpdatedBy", "UpdatedDate", "Value" },
                values: new object[,]
                {
                    { 1, false, null, "Confirm-Email", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Html/ConfirmUser.html" },
                    { 2, false, null, "Reset-Password", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Html/ResetPassword.html" },
                    { 3, false, null, "Customer-Receipt", new Guid("00000000-0000-0000-0000-000000000000"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, null, "Html/CustomerReceipt.html" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
