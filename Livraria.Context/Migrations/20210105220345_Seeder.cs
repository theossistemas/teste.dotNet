using Microsoft.EntityFrameworkCore.Migrations;

namespace Livraria.Context.Migrations
{
    public partial class Seeder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Pessoas",
                columns: new[] { "Id", "Nome" },
                values: new object[] { 1, "Administrador" });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Email", "IdPessoa", "Login", "Permissao", "Senha" },
                values: new object[] { 1, "teste@123", 1, "admin", 1, "gqefEbSstSpkLvfjOd/OSqkv9l7S56twLXmNvhDsoLg=" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Pessoas",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
