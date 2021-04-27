using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Livraria.Infra.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true),
                    Autor = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nome = table.Column<string>(type: "varchar(50)", nullable: false),
                    Editora = table.Column<string>(type: "varchar(50)", nullable: false),
                    Sinopse = table.Column<string>(type: "varchar(1000)", nullable: true),
                    Genero = table.Column<string>(type: "varchar(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro");
        }
    }
}
