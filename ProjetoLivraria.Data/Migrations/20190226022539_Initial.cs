using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjetoLivraria.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CriadoEm = table.Column<DateTime>(nullable: true),
                    ModificadoEm = table.Column<DateTime>(nullable: true),
                    Isbn = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Autor = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Titulo = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Preco = table.Column<double>(nullable: false),
                    Publicacao = table.Column<DateTime>(nullable: false),
                    ImagemCapa = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
