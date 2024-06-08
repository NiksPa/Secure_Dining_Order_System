using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiningOrderingSystem.Migrations.FoodOrderDb
{
    public partial class FoodOrderDB1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodOrderList",
                table: "FoodOrderList");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "FoodOrderList");

            migrationBuilder.DropColumn(
                name: "Calorie",
                table: "FoodOrderList");

            migrationBuilder.RenameColumn(
                name: "Contents",
                table: "FoodOrderList",
                newName: "OrderedItems");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "FoodOrderList",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryDate",
                table: "FoodOrderList",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "FoodOrderList",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodOrderList",
                table: "FoodOrderList",
                column: "OrderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FoodOrderList",
                table: "FoodOrderList");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "FoodOrderList");

            migrationBuilder.DropColumn(
                name: "DeliveryDate",
                table: "FoodOrderList");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "FoodOrderList");

            migrationBuilder.RenameColumn(
                name: "OrderedItems",
                table: "FoodOrderList",
                newName: "Contents");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "FoodOrderList",
                type: "TEXT",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Calorie",
                table: "FoodOrderList",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FoodOrderList",
                table: "FoodOrderList",
                column: "Name");
        }
    }
}
