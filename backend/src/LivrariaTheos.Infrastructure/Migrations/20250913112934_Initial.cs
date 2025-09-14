using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LivrariaTheos.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    AnoPublicacao = table.Column<int>(type: "int", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Editora = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DataPublicacao = table.Column<DateOnly>(type: "date", nullable: false),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                    table.CheckConstraint("CK_Livro_Estoque_NaoNegativo", "[Estoque] >= 0");
                    table.CheckConstraint("CK_Livro_Preco_NaoNegativo", "[Preco] >= 0");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_Titulo",
                table: "Livros",
                column: "Titulo");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_ISBN",
                table: "Livros",
                column: "ISBN",
                unique: true,
                filter: "[ISBN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Livros_Titulo_Autor_AnoPublicacao",
                table: "Livros",
                columns: new[] { "Titulo", "Autor", "AnoPublicacao" },
                unique: true,
                filter: "[AnoPublicacao] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
