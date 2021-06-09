using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TMSA.Livraria.Domain.Interfaces;
using TMSA.Livraria.Domain.Models;
using TMSA.Livraria.Infra.Data.Connection;

namespace TMSA.Livraria.Infra.Data.Repository
{
    public class LivroRepository : ConnectionDB, ILivroRepository
    {
        public LivroRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task AtualizarLivro(Livro livro)
        {
            using (var con = Connection)
            {
                string sqlAtualizarLivro = @"UPDATE LIVRO SET ISBN = @ISBN,
                                                              TITULO = @TITULO,
                                                              GENERO = @GENERO,
                                                              QTD_PAGINAS = @QTD_PAGINAS,
                                                              NOME_AUTOR = @NOME_AUTOR
                                             WHERE
                                                     LIVROID =  @LIVROID";

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("LIVROID", livro.LivroId, DbType.Guid, ParameterDirection.Input);
                    parametros.Add("ISBN", livro.ISBN, DbType.String, ParameterDirection.Input);
                    parametros.Add("TITULO", livro.Titulo, DbType.String, ParameterDirection.Input);
                    parametros.Add("GENERO", livro.Genero, DbType.String, ParameterDirection.Input);
                    parametros.Add("QTD_PAGINAS", livro.QuantidadeDePaginas, DbType.Int32, ParameterDirection.Input);
                    parametros.Add("NOME_AUTOR", livro.NomeDoAutor, DbType.String, ParameterDirection.Input);

                    await con.ExecuteAsync(sqlAtualizarLivro, param: parametros);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public async Task CriarLivro(Livro livro)
        {
            using (var con = Connection)
            {
                string sqlCriarLivro = @"INSERT INTO LIVRO
                                        (
                                            LIVROID,
                                            ISBN,
                                            TITULO,
                                            GENERO,
                                            QTD_PAGINAS,
                                            NOME_AUTOR
                                        )
                                        VALUES 
                                        (
                                            @LIVROID,
                                            @ISBN,
                                            @TITULO,
                                            @GENERO,
                                            @QTD_PAGINAS,
                                            @NOME_AUTOR
                                        )";

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("LIVROID", livro.LivroId, DbType.Guid, ParameterDirection.Input);
                    parametros.Add("ISBN", livro.ISBN, DbType.String, ParameterDirection.Input);
                    parametros.Add("TITULO", livro.Titulo, DbType.String, ParameterDirection.Input);
                    parametros.Add("GENERO", livro.Genero, DbType.String, ParameterDirection.Input);
                    parametros.Add("QTD_PAGINAS", livro.QuantidadeDePaginas, DbType.Int32, ParameterDirection.Input);
                    parametros.Add("NOME_AUTOR", livro.NomeDoAutor, DbType.String, ParameterDirection.Input);

                    await con.ExecuteAsync(sqlCriarLivro, param: parametros);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }

        public async Task<Livro> ObterLivroPorId(Guid livroId)
        {
            using (var con = Connection)
            {
                string sqlObterLivroPorId = @"SELECT 
                                                    L.LIVROID AS LivroId,
                                                    L.ISBN AS ISBN,
                                                    L.TITULO AS Titulo,
                                                    L.GENERO AS Genero,
                                                    L.QTD_PAGINAS AS QuantidadeDePaginas,
                                                    L.NOME_AUTOR AS NomeDoAutor
                                             FROM
                                                    LIVRO L
                                             WHERE
                                                    L.LIVROID = @LIVRO_ID
                                             ORDER BY L.TITULO ASC";

                Livro livro = default(Livro);

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("LIVRO_ID", livroId, DbType.Guid, ParameterDirection.Input);

                    livro = await con.QueryFirstOrDefaultAsync<Livro>(sqlObterLivroPorId, param: parametros);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

                return livro;
            }
        }

        public async Task<Livro> ObterLivroPorISBN(string isbn)
        {
            using (var con = Connection)
            {
                string sqlObterLivroPorISBN = @"SELECT 
                                                    L.LIVROID AS LivroId,
                                                    L.ISBN AS ISBN,
                                                    L.TITULO AS Titulo,
                                                    L.GENERO AS Genero,
                                                    L.QTD_PAGINAS AS QuantidadeDePaginas,
                                                    L.NOME_AUTOR AS NomeDoAutor
                                             FROM
                                                    LIVRO L
                                             WHERE
                                                    L.ISBN = @LIVRO_ISBN";

                Livro livro = default(Livro);

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("LIVRO_ISBN", isbn, DbType.String, ParameterDirection.Input);

                    livro = await con.QueryFirstOrDefaultAsync<Livro>(sqlObterLivroPorISBN, param: parametros);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

                return livro;
            }
        }

        public async Task<IEnumerable<Livro>> ObterLivros()
        {
            using (var con = Connection)
            {
                string sqlObterLivros = @"SELECT 
                                                L.LIVROID AS LivroId,
                                                L.ISBN AS ISBN,
                                                L.TITULO AS Titulo,
                                                L.GENERO AS Genero,
                                                L.QTD_PAGINAS AS QuantidadeDePaginas,
                                                L.NOME_AUTOR AS NomeDoAutor
                                         FROM
                                                LIVRO L";

                IEnumerable<Livro> livros = default(IEnumerable<Livro>);

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    livros = await con.QueryAsync<Livro>(sqlObterLivros);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }

                return livros;
            }
        }

        public async Task RemoverLivro(Guid livroId)
        {
            using (var con = Connection)
            {
                string sqlRemoverLivro = @"DELETE LIVRO WHERE LIVROID = @LIVROID";

                try
                {
                    if (con.State == ConnectionState.Closed)
                        con.Open();

                    DynamicParameters parametros = new DynamicParameters();
                    parametros.Add("LIVROID", livroId, DbType.Guid, ParameterDirection.Input);

                    await con.ExecuteAsync(sqlRemoverLivro, param: parametros);
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    con.Close();
                    con.Dispose();
                }
            }
        }
    }
}
