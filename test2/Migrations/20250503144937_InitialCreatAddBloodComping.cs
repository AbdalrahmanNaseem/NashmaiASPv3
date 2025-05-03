using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace test2.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreatAddBloodComping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BloodCompings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: true),
                    Goal = table.Column<int>(type: "int", nullable: true),
                    BloodCategoryId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodCompings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodCompings_BloodCategory_BloodCategoryId",
                        column: x => x.BloodCategoryId,
                        principalTable: "BloodCategory",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BloodCompings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BloodCompings_BloodCategoryId",
                table: "BloodCompings",
                column: "BloodCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_BloodCompings_UserId",
                table: "BloodCompings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodCompings");
        }
    }
}
