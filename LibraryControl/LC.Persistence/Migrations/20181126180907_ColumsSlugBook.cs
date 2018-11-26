using Microsoft.EntityFrameworkCore.Migrations;

namespace LC.Persistence.Migrations
{
    public partial class ColumsSlugBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SLUG",
                table: "TB_BOOKS",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TB_BOOKS_SLUG",
                table: "TB_BOOKS",
                column: "SLUG",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_BOOKS_SLUG",
                table: "TB_BOOKS");

            migrationBuilder.DropColumn(
                name: "SLUG",
                table: "TB_BOOKS");
        }
    }
}
