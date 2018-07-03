using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaTemp.Migrations
{
    public partial class MudandoIdTabelaLivros : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Livros",
                newName: "LivroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LivroId",
                table: "Livros",
                newName: "Id");
        }
    }
}
