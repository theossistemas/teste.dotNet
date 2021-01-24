using Microsoft.EntityFrameworkCore.Migrations;

namespace MaiaraBookstore.Migrations
{
    public partial class IncluidoColunaDescricaoTabelaLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LogDescricao",
                table: "LogBookstores",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LogDescricao",
                table: "LogBookstores");
        }
    }
}
