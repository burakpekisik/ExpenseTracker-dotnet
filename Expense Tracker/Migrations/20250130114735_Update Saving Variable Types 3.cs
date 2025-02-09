using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavingVariableTypes3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "SavingCategories",
                type: "nvarchar(10)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "CurrentPrice",
                table: "SavingCategories",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "TotalValue",
                table: "SavingCategories",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "SavingCategories");

            migrationBuilder.DropColumn(
                name: "CurrentPrice",
                table: "SavingCategories");

            migrationBuilder.DropColumn(
                name: "TotalValue",
                table: "SavingCategories");
        }
    }
}
