using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaAsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class UPDATETEMPLATEDEFAULTCREATOR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActiveStatus", "CreatedBy" },
                values: new object[] { true, new Guid("c325b987-a6ce-4462-9116-f76922e7206c") });

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActiveStatus", "CreatedBy" },
                values: new object[] { true, new Guid("c325b987-a6ce-4462-9116-f76922e7206c") });

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActiveStatus", "CreatedBy" },
                values: new object[] { true, new Guid("c325b987-a6ce-4462-9116-f76922e7206c") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ActiveStatus", "CreatedBy" },
                values: new object[] { false, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ActiveStatus", "CreatedBy" },
                values: new object[] { false, new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ActiveStatus", "CreatedBy" },
                values: new object[] { false, new Guid("00000000-0000-0000-0000-000000000000") });
        }
    }
}
