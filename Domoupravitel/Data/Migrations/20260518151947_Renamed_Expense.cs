using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domoupravitel.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Expense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Cleaning",
                table: "Expenses",
                newName: "Vault");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Vault",
                table: "Expenses",
                newName: "Cleaning");
        }
    }
}
