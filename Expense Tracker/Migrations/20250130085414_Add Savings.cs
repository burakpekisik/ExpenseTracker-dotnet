using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Expense_Tracker.Migrations
{
    /// <inheritdoc />
    public partial class AddSavings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SavingCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(5)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SavingCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Savings",
                columns: table => new
                {
                    SavingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SavingCategoryId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    AmountYesterday = table.Column<int>(type: "int", nullable: false),
                    AmountToday = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Savings", x => x.SavingId);
                    table.ForeignKey(
                        name: "FK_Savings_SavingCategories_SavingCategoryId",
                        column: x => x.SavingCategoryId,
                        principalTable: "SavingCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Savings_SavingCategoryId",
                table: "Savings",
                column: "SavingCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Savings");

            migrationBuilder.DropTable(
                name: "SavingCategories");
        }
    }
}
