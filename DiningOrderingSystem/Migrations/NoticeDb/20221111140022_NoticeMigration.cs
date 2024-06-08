using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DiningOrderingSystem.Migrations.NoticeDb
{
    public partial class NoticeMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NoticeItemList",
                columns: table => new
                {
                    NoticeTitle = table.Column<string>(type: "TEXT", nullable: false),
                    NoticeContent = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoticeItemList", x => x.NoticeTitle);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NoticeItemList");
        }
    }
}
