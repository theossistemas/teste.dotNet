using Dapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Utils.Connection;
using Utils.Exceptions;
using Utils.Exceptions.Atualizacao;

namespace Repositories.Atualizacoes
{
    public class AtualizacaoRepository
    {
        public void AtualizarSistemaPorListaAtualizacoes(IList<IAtualizacaoAutomatica> atualizacoes)
        {
            this.VerificarBancoDeDados();

            IAtualizacaoAutomatica atualizacaoAtual = null;

            using (IDbConnection conn = SqlServerHelper.Connection)
            using (IDbTransaction transaction = conn.BeginTransaction())
            {
                try
                {
                    this.ExecutarListaAtualizacoes(conn, transaction, atualizacoes, out atualizacaoAtual);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw new AtualizacaoAutomaticaException
                    (
                        atualizacaoAtual?.Versao.Guid,
                        (atualizacaoAtual?.Versao.Numero).GetValueOrDefault(),
                        ex.ToString()
                    ).GravarLog();
                }
            }
        }

        public Boolean VerificarSeAtualizacaoNaoFoiRealizada(IAtualizacaoAutomatica atualizacao)
        {
            try
            {
                using (IDbConnection conn = SqlServerHelper.Connection)
                {
                    return 0.Equals(
                        conn.Query<Int32>(@"SELECT COUNT(Id) FROM Versao WHERE Guid = @guid AND Numero = @numero", new
                        {
                            guid = atualizacao.Versao.Guid,
                            numero = atualizacao.Versao.Numero
                        }).FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                throw ex.GravarLog();
            }
        }

        public Boolean VerificarSeAtualizacaoNaoFoiRealizada(IDbConnection connection, IDbTransaction transaction, IAtualizacaoAutomatica atualizacao)
        {
            try
            {
                return 0.Equals(
                    connection.Query<Int32>(@"SELECT COUNT(Id) FROM Versao WHERE Guid = @guid AND Numero = @numero", new
                    {
                        guid = atualizacao.Versao.Guid,
                        numero = atualizacao.Versao.Numero
                    },
                    transaction: transaction).FirstOrDefault());
            }
            catch (Exception ex)
            {
                throw ex.GravarLog();
            }
        }

        private void ExecutarListaAtualizacoes(IDbConnection connection, IDbTransaction transaction, IList<IAtualizacaoAutomatica> atualizacoes, out IAtualizacaoAutomatica atualizacaoAtual)
        {
            IAtualizacaoAutomatica atualizacaoOut = null;

            try
            {
                foreach (IAtualizacaoAutomatica atualizacao in atualizacoes.OrderBy(x => x.Versao.Numero))
                {
                    if (this.VerificarSeAtualizacaoNaoFoiRealizada(connection, transaction, atualizacao))
                    {
                        atualizacaoOut = atualizacao;

                        if (String.IsNullOrEmpty(atualizacao.ComandoExcluir))
                            connection.Execute(atualizacao.ComandoExcluir, transaction: transaction);

                        connection.Execute(atualizacao.ComandoCriar, transaction: transaction);

                        connection.Execute(@"INSERT INTO Versao (Guid, Numero) VALUES (@guid, @numero)", new
                        {
                            guid = atualizacao.Versao.Guid,
                            numero = atualizacao.Versao.Numero
                        },
                        transaction: transaction);
                    }
                }
            }
            finally
            {
                atualizacaoAtual = atualizacaoOut;
            }
        }

        private void VerificarBancoDeDados()
        {
            if (!this.VerificarSeBancoDeDadosExiste("Theos_teste"))
            {
                this.CriarBancoDados();
            }

            SqlServerHelper.ConnectionStringBuilder.InitialCatalog = "Theos_teste";

            if (!this.VerificarSeTabelaExiste("Versao"))
            {
                this.CriarTabelaVersao();
            }
        }

        public Boolean VerificarSeBancoDeDadosExiste(String nomeDb)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
            {
                return connection.Query<Int32>("SELECT COUNT(*) FROM sys.databases WHERE name = @nomeDb", new { nomeDb })
                    .FirstOrDefault() > 0;
            }
        }

        public Boolean VerificarSeTabelaExiste(String nomeTabela)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
            {
                return connection.Query<Int32>("SELECT COUNT(*) FROM sys.tables WHERE name = @nomeTabela", new { nomeTabela })
                    .FirstOrDefault() > 0;
            }
        }

        private void CriarBancoDados()
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
            {
                try
                {
                    connection.Execute("CREATE DATABASE Theos_teste");
                }
                catch (Exception ex)
                {
                    throw ex.GravarLog();
                }
            }
        }

        private void CriarTabelaVersao()
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
            {
                try
                {
                    connection.Execute("CREATE TABLE Versao (Id BIGINT IDENTITY PRIMARY KEY, Guid VARCHAR(100) NOT NULL UNIQUE, Numero BIGINT NOT NULL)");
                }
                catch (Exception ex)
                {
                    throw ex.GravarLog();
                }
            }
        }
    }
}
