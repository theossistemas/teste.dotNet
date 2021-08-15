using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace LivrariaTheos.Estoque.Data.Migrations
{
    public partial class Add_Tabela_Log_Aplicacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogAplicacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Metodo = table.Column<string>(type: "varchar(250)", nullable: false),                   
                    MensagemErro = table.Column<string>(type: "varchar(max)", nullable: true),
                    StackTrace = table.Column<string>(type: "varchar(max)", nullable: true),                    
                    UsuarioInclusao = table.Column<string>(type: "varchar(100)", nullable: true),
                    DataInclusao = table.Column<DateTime>(type: "datetime2", nullable: false),
                   
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogAplicacao", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogAplicacao");
        }
    }
}
