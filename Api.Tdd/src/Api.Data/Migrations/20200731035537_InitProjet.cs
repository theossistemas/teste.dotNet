using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InitProjet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataCriacao = table.Column<DateTime>(nullable: true),
                    DataAlteracao = table.Column<DateTime>(nullable: true),
                    Ativo = table.Column<bool>(nullable: false),
                    Titulo = table.Column<string>(maxLength: 100, nullable: true),
                    Autor = table.Column<string>(maxLength: 100, nullable: true),
                    Categoria = table.Column<string>(maxLength: 100, nullable: true),
                    Emissora = table.Column<string>(maxLength: 100, nullable: true),
                    DataLancamento = table.Column<DateTime>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false),
                    Quantidade = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Politicas",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(maxLength: 256, nullable: true),
                    NomeNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    Concorrencia = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Politicas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Login = table.Column<string>(maxLength: 256, nullable: true),
                    LoginNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    EmailNormalizado = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmado = table.Column<bool>(nullable: false),
                    SenhaHash = table.Column<string>(nullable: true),
                    Seguranca = table.Column<string>(nullable: true),
                    Concorrencia = table.Column<string>(nullable: true),
                    TelefoneNumero = table.Column<string>(nullable: true),
                    TelefoneConfirmado = table.Column<bool>(nullable: false),
                    VerificaoDoidPassos = table.Column<bool>(nullable: false),
                    FimBloqueio = table.Column<DateTimeOffset>(nullable: true),
                    BloqueioAtivo = table.Column<bool>(nullable: false),
                    AcessoFalhas = table.Column<int>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    Sobrenome = table.Column<string>(nullable: true),
                    Telefone = table.Column<string>(nullable: true),
                    IdPerfil = table.Column<int>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PoliticasClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPolitica = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PoliticasClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PoliticasClaims_Politicas_IdPolitica",
                        column: x => x.IdPolitica,
                        principalTable: "Politicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosLogins",
                columns: table => new
                {
                    LoginProvavel = table.Column<string>(nullable: false),
                    ChaveProvavel = table.Column<string>(nullable: false),
                    NomeProvavel = table.Column<string>(nullable: true),
                    IdUsuario = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosLogins", x => new { x.LoginProvavel, x.ChaveProvavel });
                    table.ForeignKey(
                        name: "FK_UsuariosLogins_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPapeis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(nullable: false),
                    Tipo = table.Column<string>(nullable: true),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPapeis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UsuariosPapeis_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosPoliticas",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false),
                    IdPolitica = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosPoliticas", x => new { x.IdUsuario, x.IdPolitica });
                    table.ForeignKey(
                        name: "FK_UsuariosPoliticas_Politicas_IdPolitica",
                        column: x => x.IdPolitica,
                        principalTable: "Politicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuariosPoliticas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuariosTokens",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(nullable: false),
                    LoginProvavel = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: false),
                    Valor = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuariosTokens", x => new { x.IdUsuario, x.LoginProvavel, x.Nome });
                    table.ForeignKey(
                        name: "FK_UsuariosTokens_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Politicas",
                column: "NomeNormalizado",
                unique: true,
                filter: "[NomeNormalizado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PoliticasClaims_IdPolitica",
                table: "PoliticasClaims",
                column: "IdPolitica");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Usuarios",
                column: "EmailNormalizado");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Usuarios",
                column: "LoginNormalizado",
                unique: true,
                filter: "[LoginNormalizado] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosLogins_IdUsuario",
                table: "UsuariosLogins",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPapeis_IdUsuario",
                table: "UsuariosPapeis",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_UsuariosPoliticas_IdPolitica",
                table: "UsuariosPoliticas",
                column: "IdPolitica");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "PoliticasClaims");

            migrationBuilder.DropTable(
                name: "UsuariosLogins");

            migrationBuilder.DropTable(
                name: "UsuariosPapeis");

            migrationBuilder.DropTable(
                name: "UsuariosPoliticas");

            migrationBuilder.DropTable(
                name: "UsuariosTokens");

            migrationBuilder.DropTable(
                name: "Politicas");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
