using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasaAsa.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDefaultAddressColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultAddress",
                table: "Addresses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefaultAddress",
                table: "Addresses");
        }
    }
}
