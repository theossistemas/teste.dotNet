using CatalogoLivros.Models;
using Microsoft.EntityFrameworkCore.Migrations;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace CatalogoLivros.Migrations
{
    /// <inheritdoc />
    public partial class PopulaLivros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO LIVROS(NOME, AUTOR, ANO, EDITORA, DATACADASTRO, QTDEESTOQUE, GENEROID)" +
                "VALUES('Dom Casmurro','Machado de Assis',1899,'Editora Nova Aguilar',now(),1,8)");
            mb.Sql("INSERT INTO LIVROS(NOME, AUTOR, ANO, EDITORA, DATACADASTRO, QTDEESTOQUE, GENEROID)" +
                "VALUES('Steve Jobs','Walter Isaacson',2011, Companhia das Letras',now(),1,1)");
            mb.Sql("INSERT INTO LIVROS(NOME, AUTOR, ANO, EDITORA, DATACADASTRO, QTDEESTOQUE, GENEROID)" +
                "VALUES('O Aleph','Jorge Luis Borges',1949,'Companhia das Letras',now(),1,2)");
            mb.Sql("INSERT INTO LIVROS(NOME, AUTOR, ANO, EDITORA, DATACADASTRO, QTDEESTOQUE, GENEROID)" +
                "VALUES('Turma da Mônica: Laços','Vitor Cafaggi e Lu Cafaggi',2013,'Panini Comics',now(),1,3)");
            mb.Sql("INSERT INTO LIVROS(NOME, AUTOR, ANO, EDITORA, DATACADASTRO, QTDEESTOQUE, GENEROID)" +
                "VALUES('Orgulho e Preconceito','Jane Austen',1813,'Martin Claret',now(),1,8)");
            mb.Sql("INSERT INTO LIVROS(NOME, AUTOR, ANO, EDITORA, DATACADASTRO, QTDEESTOQUE, GENEROID)" +
                "VALUES('Antologia Poética','Vinicius de Moraes',1954,'Companhia das Letras',now(),1,4)");
            mb.Sql("INSERT INTO LIVROS(NOME, AUTOR, ANO, EDITORA, DATACADASTRO, QTDEESTOQUE, GENEROID)" +
                "VALUES('O Pequeno Príncipe','Antoine de Saint-Exupéry',1943,'Agir',now(),1,6)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM LIVROS");
        }
    }
}