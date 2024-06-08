using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiningOrderingSystem.Migrations.FoodOrderDb
{
    public partial class FoodOrderDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentName",
                table: "FoodOrderList",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentName",
                table: "FoodOrderList");
        }
    }
}
