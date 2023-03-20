using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharma.EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantityy",
                table: "Sales",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Sales",
                newName: "Quantityy");
        }
    }
}
