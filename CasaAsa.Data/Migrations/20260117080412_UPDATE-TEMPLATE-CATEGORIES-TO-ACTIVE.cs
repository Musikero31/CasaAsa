using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaAsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class UPDATETEMPLATECATEGORIESTOACTIVE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "ActiveStatus",
                value: true);

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "ActiveStatus",
                value: true);

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "ActiveStatus",
                value: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 1,
                column: "ActiveStatus",
                value: false);

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 2,
                column: "ActiveStatus",
                value: false);

            migrationBuilder.UpdateData(
                table: "ApplicationSettings",
                keyColumn: "Id",
                keyValue: 3,
                column: "ActiveStatus",
                value: false);
        }
    }
}
