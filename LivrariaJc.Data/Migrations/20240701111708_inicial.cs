using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LivrariaJc.Data.Migrations
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Autor = table.Column<string>(type: "nvarchar(140)", maxLength: 140, nullable: true),
                    Valor = table.Column<decimal>(type: "decimal(10,4)", precision: 10, scale: 4, nullable: false),
                    DataLancamento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataCriacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Autor", "DataAtualizacao", "DataCriacao", "DataLancamento", "Titulo", "Valor" },
                values: new object[,]
                {
                    { 1, "William Shakespeare", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1875, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Romeu e Julieta", 160.00m },
                    { 2, "Pero Vaz de Caminha", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1842, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Carta de Pero Vaz de Caminha", 100.00m },
                    { 3, "Afonso Henriques de Lima Barreto", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1985, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Triste Fim de Policarpo Quaresma: análise, contexto histórico e mais", 50.00m },
                    { 4, "Gil Vicente", null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2001, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Auto da Barca do Inferno", 40.00m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");
        }
    }
}
