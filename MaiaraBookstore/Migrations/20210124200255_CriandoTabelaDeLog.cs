using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MaiaraBookstore.Migrations
{
    public partial class CriandoTabelaDeLog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogBookstores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    LivroId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogBookstores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LogBookstores_Livro_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LogBookstores_LivroId",
                table: "LogBookstores",
                column: "LivroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogBookstores");
        }
    }
}
