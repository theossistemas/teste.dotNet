using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaTheos.Estoque.Data.Migrations
{
    public partial class Init_Database : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Nacionalidade = table.Column<int>(type: "int", nullable: false),
                    InformacoesRelevantes = table.Column<string>(type: "varchar(2000)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UsuarioInclusao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioAlteracao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genero",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UsuarioInclusao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioAlteracao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AutorId = table.Column<int>(type: "int", nullable: false),
                    GeneroId = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "varchar(250)", nullable: false),
                    Sinopse = table.Column<string>(type: "varchar(2000)", nullable: false),
                    QuantidadePaginas = table.Column<int>(type: "int", nullable: false),
                    CaminhoCapa = table.Column<string>(type: "varchar(350)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UsuarioInclusao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioAlteracao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataAlteracao = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Livro_Autor_AutorId",
                        column: x => x.AutorId,
                        principalTable: "Autor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Livro_Genero_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Genero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Autor",
                columns: new[] { "Id", "Ativo", "DataAlteracao", "DataInclusao", "InformacoesRelevantes", "Nacionalidade", "Nome", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Informações sobre o autor", 3, "Stephen King", null, "Seed" },
                    { 2, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Informações sobre o autor", 1, "Paulo Coelho", null, "Seed" },
                    { 3, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Informações sobre o autor", 6, "Akira Toryama", null, "Seed" },
                    { 4, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Informações sobre o autor", 5, "Thomas Mann", null, "Seed" },
                    { 5, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Informações sobre o autor", 4, "J.K Rowling", null, "Seed" }
                });

            migrationBuilder.InsertData(
                table: "Genero",
                columns: new[] { "Id", "Ativo", "DataAlteracao", "DataInclusao", "Nome", "UsuarioAlteracao", "UsuarioInclusao" },
                values: new object[,]
                {
                    { 1, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Drama", null, "Seed" },
                    { 2, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Mistério/Suspense", null, "Seed" },
                    { 3, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Romance", null, "Seed" },
                    { 4, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Épico/Aventura", null, "Seed" },
                    { 5, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Distopia", null, "Seed" },
                    { 6, true, null, new DateTime(2021, 1, 1, 1, 1, 1, 0, DateTimeKind.Unspecified), "Fantasia/Sobrenatural", null, "Seed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livro_AutorId",
                table: "Livro",
                column: "AutorId");

            migrationBuilder.CreateIndex(
                name: "IX_Livro_GeneroId",
                table: "Livro",
                column: "GeneroId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "Autor");

            migrationBuilder.DropTable(
                name: "Genero");
        }
    }
}
