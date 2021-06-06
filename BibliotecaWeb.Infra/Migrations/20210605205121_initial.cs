using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LivrariaWeb.Infra.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Livro",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NomeAutor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    data_publicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    numero_paginas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livro", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pessoa",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nome_pessoa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    senha = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pessoa", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Livro");

            migrationBuilder.DropTable(
                name: "Pessoa");
        }
    }
}
