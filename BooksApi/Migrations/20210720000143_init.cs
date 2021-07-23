using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(80)", nullable: false),
                    TotalPagina = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    Autor = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Promocao = table.Column<bool>(type: "bit", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ValorPromocao = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    ImagemUrl = table.Column<string>(maxLength: 100, nullable: true),
                    Resumo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "VARCHAR(80)", nullable: false),
                    Cpf = table.Column<string>(nullable: true),
                    Email = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    Senha = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    NomeUsuario = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Role = table.Column<string>(type: "VARCHAR(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Livros",
                columns: new[] { "Id", "Autor", "ImagemUrl", "Isbn", "Promocao", "Resumo", "Titulo", "TotalPagina", "Valor", "ValorPromocao" },
                values: new object[,]
                {
                    { 1, "PEDRO SILVA", "", "123456785", false, "mauris cursus mattis molestie a iaculis at erat pellentesque adipiscing commodo elit at imperdiet dui accumsan sit amet nulla facilisi morbi tempus iaculis urna id volutpat lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis aenean et tortor at risus viverra adipiscing at in tellus integer feugiat scelerisque varius", "Net Core 5.0", 225, 150m, 0m },
                    { 2, "JOSEFA ANTONIA", "", "123456789101579", false, "mauris cursus mattis molestie a iaculis at erat pellentesque adipiscing commodo elit at imperdiet dui accumsan sit amet nulla facilisi morbi tempus iaculis urna id volutpat lacus laoreet non curabitur gravida arcu ac tortor dignissim convallis aenean et tortor at risus viverra adipiscing at in tellus integer feugiat scelerisque varius", "React ", 250, 80m, 0m },
                    { 3, "GRACILIANO RAMOS", "", "753214789", true, "", "Angustia", 157, 35m, 19m }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Cpf", "Email", "Nome", "NomeUsuario", "Role", "Senha" },
                values: new object[] { 1, null, "admin@email.com", "Administrador", "admin", "admin", "123456789" });

            migrationBuilder.CreateIndex(
                name: "IDX_ISBN",
                table: "Livros",
                column: "Isbn",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IDX_TITLE",
                table: "Livros",
                column: "Titulo");

            migrationBuilder.CreateIndex(
                name: "IDX_EMAIL",
                table: "Usuarios",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
