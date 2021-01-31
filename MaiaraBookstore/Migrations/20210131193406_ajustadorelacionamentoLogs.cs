using Microsoft.EntityFrameworkCore.Migrations;

namespace MaiaraBookstore.Migrations
{
    public partial class ajustadorelacionamentoLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LogBookstores_Livro_LivroId",
                table: "LogBookstores");

            migrationBuilder.DropIndex(
                name: "IX_LogBookstores_LivroId",
                table: "LogBookstores");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_LogBookstores_LivroId",
                table: "LogBookstores",
                column: "LivroId");

            migrationBuilder.AddForeignKey(
                name: "FK_LogBookstores_Livro_LivroId",
                table: "LogBookstores",
                column: "LivroId",
                principalTable: "Livro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
