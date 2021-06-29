using Microsoft.EntityFrameworkCore.Migrations;

namespace Livraria.Data.Migrations
{
    public partial class AddBookInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BookInfo",
                table: "Books",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookInfo",
                table: "Books");
        }
    }
}
