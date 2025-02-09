using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSavings2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountToday",
                table: "Savings");

            migrationBuilder.DropColumn(
                name: "AmountYesterday",
                table: "Savings");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPerUnit",
                table: "Savings",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "SavingCategories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "CostPerUnit",
                table: "SavingCategories",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCost",
                table: "SavingCategories",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "SavingCategories");

            migrationBuilder.DropColumn(
                name: "CostPerUnit",
                table: "SavingCategories");

            migrationBuilder.DropColumn(
                name: "TotalCost",
                table: "SavingCategories");

            migrationBuilder.AlterColumn<decimal>(
                name: "CostPerUnit",
                table: "Savings",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "AmountToday",
                table: "Savings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AmountYesterday",
                table: "Savings",
                type: "int",
                nullable: true);
        }
    }
}
