using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Livraria.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class news : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Livros",
                table: "Livros");

            migrationBuilder.RenameTable(
                name: "Livros",
                newName: "Livraria");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livraria",
                table: "Livraria",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Livraria",
                table: "Livraria");

            migrationBuilder.RenameTable(
                name: "Livraria",
                newName: "Livros");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Livros",
                table: "Livros",
                column: "Id");
        }
    }
}
