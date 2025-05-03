using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreateAddGoalAndAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Compings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Goal",
                table: "Compings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Compings");

            migrationBuilder.DropColumn(
                name: "Goal",
                table: "Compings");
        }
    }
}
