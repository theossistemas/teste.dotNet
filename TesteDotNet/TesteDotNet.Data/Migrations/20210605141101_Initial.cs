using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TesteDotNet.Data.Migrations
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
                    Nome = table.Column<string>(type: "varchar(100)", nullable: false),
                    Categoria = table.Column<string>(type: "varchar(50)", nullable: false),
                    Autor = table.Column<string>(type: "varchar(100)", nullable: false),
                    DataLancamento = table.Column<DateTime>(nullable: false),
                    Edicao = table.Column<string>(type: "varchar(50)", nullable: true)
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
