using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiningOrderingSystem.Migrations.FoodItemDb
{
    public partial class FoodItemMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodItemList",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Calorie = table.Column<int>(type: "INTEGER", nullable: false),
                    Contents = table.Column<string>(type: "TEXT", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodItemList", x => x.Name);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodItemList");
        }
    }
}
