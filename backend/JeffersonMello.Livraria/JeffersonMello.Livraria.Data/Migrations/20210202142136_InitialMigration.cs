using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JeffersonMello.Livraria.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cad_categoria",
                columns: table => new
                {
                    guid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datacriacao = table.Column<DateTime>(nullable: false),
                    dataalteracao = table.Column<DateTime>(nullable: false),
                    descricao = table.Column<string>(maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_categoria", x => x.guid);
                });

            migrationBuilder.CreateTable(
                name: "cad_livro",
                columns: table => new
                {
                    guid = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    datacriacao = table.Column<DateTime>(nullable: false),
                    dataalteracao = table.Column<DateTime>(nullable: false),
                    titulo = table.Column<string>(maxLength: 100, nullable: false),
                    descricao = table.Column<string>(nullable: false),
                    autor = table.Column<string>(maxLength: 150, nullable: false),
                    marca = table.Column<string>(maxLength: 100, nullable: true),
                    datalancamento = table.Column<DateTime>(nullable: false),
                    guid_categoria = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cad_livro", x => x.guid);
                    table.ForeignKey(
                        name: "FK_cad_livro_cad_categoria_guid_categoria",
                        column: x => x.guid_categoria,
                        principalTable: "cad_categoria",
                        principalColumn: "guid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cad_livro_guid_categoria",
                table: "cad_livro",
                column: "guid_categoria");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cad_livro");

            migrationBuilder.DropTable(
                name: "cad_categoria");
        }
    }
}
