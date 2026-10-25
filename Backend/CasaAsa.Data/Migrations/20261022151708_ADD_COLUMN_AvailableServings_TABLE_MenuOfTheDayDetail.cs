using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaAsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class ADD_COLUMN_AvailableServings_TABLE_MenuOfTheDayDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AvailableServings",
                table: "MenuOfTheDayDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableServings",
                table: "MenuOfTheDayDetail");
        }
    }
}
