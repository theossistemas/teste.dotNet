using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CatalogoLivros.Migrations
{
    /// <inheritdoc />
    public partial class PopulaGeneros : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Biografias e Autobiografias')");
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Contos')");
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Gibis')");
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Livros de poesia')");
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Livros didáticos')");
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Livros infantis')");
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Religiosos')");
            mb.Sql("INSERT INTO GENEROS(NOME) VALUES('Romances')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM GENEROS");
        }
    }
}
