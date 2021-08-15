using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaTheos.Estoque.Data.Migrations
{
    public partial class Seed_Livros_And_Add_Coluna_Nome_Capa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CaminhoCapa",
                table: "Livro",
                type: "varchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(350)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NomeCapa",
                table: "Livro",
                type: "varchar(150)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Genero",
                columns: new[] { "Id", "Ativo", "DataAlteracao", "DataInclusao", "Nome", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[] { 7, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Terror", null, "Seed" });

            migrationBuilder.InsertData(
                table: "Livro",
                columns: new[] { "Id", "Ativo", "AutorId", "CaminhoCapa", "DataAlteracao", "DataInclusao", "GeneroId", "Nome", "NomeCapa", "QuantidadePaginas", "Sinopse", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { 1, true, 5, "StaticFiles\\Capas", null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), 4, "Harry Potter e o Prisioneiro de Azkaban", "ID_1", 250, "Sinopse de Harry Potter", null, "Seed" },
                    { 2, true, 4, "StaticFiles\\Capas", null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), 1, "A Montanha Mágica", "ID_2", 250, "Sinopse de a Montanha Mágica", null, "Seed" },
                    { 3, true, 1, "StaticFiles\\Capas", null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), 5, "O Iluminado", "ID_1", 250, "Sinopse de O Iluminado", null, "Seed" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genero",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Livro",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Livro",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Livro",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "NomeCapa",
                table: "Livro");

            migrationBuilder.AlterColumn<string>(
                name: "CaminhoCapa",
                table: "Livro",
                type: "varchar(350)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(500)",
                oldNullable: true);
        }
    }
}
