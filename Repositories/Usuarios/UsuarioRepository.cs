using Dapper;
using Entities;
using Enumerators;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Utils.Connection;
using Utils.Exceptions;
using Utils.Exceptions.Usuario;

namespace Repositories.Usuarios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        public void Delete(Int64? id)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    connection.Execute(@"DELETE FROM Usuario WHERE Id = @id", new { id }, transaction: transaction);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw ex.GravarLog();
                }
            }
        }

        public Usuario Find(Int64? id)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return connection.Query<Usuario>(@"SELECT * FROM Usuario WHERE Id = @id", new { id })
                    .FirstOrDefault();
        }

        public IList<Usuario> FindAll()
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return connection.Query<Usuario>(@"SELECT * FROM Usuario ORDER BY Login ASC")
                    .ToList();
        }

        public Usuario Save(Usuario usuario)
        {
            this.VerificarSeUsuarioJaCadastrado(usuario.Login);

            using (IDbConnection connection = SqlServerHelper.Connection)
            using (IDbTransaction transaction = connection.BeginTransaction())
            {
                try
                {
                    usuario.Id = connection.Query<Int64>(@"EXEC PROC_SalvarAtualizarUsuario(@id, @login, @senha, @permissao)", new
                    {
                        id = usuario.Id,
                        login = usuario.Login,
                        senha = usuario.Senha,
                        permissao = usuario.Permissao
                    },
                    commandType: CommandType.StoredProcedure,
                    transaction: transaction)
                    .FirstOrDefault();

                    transaction.Commit();

                    return usuario;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();

                    throw ex.GravarLog();
                }
            }
        }

        public Boolean ValidarLogin(String login, String senha)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return connection.Query<Int32>("SELECT COUNT(Id) FROM Usuario WHERE Login = @login AND Senha = @senha", new
                {
                    login,
                    senha
                }).FirstOrDefault() > 0;
        }

        public Permissao? RetornarPermissaoDoUsuario(String login)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return (Permissao?)connection.Query<Int16?>("SELECT Permissao FROM Usuario WHERE Login = @login", new { login })
                    .FirstOrDefault();
        }

        public void VerificarSeUsuarioJaCadastrado(String login)
        {
            Boolean existe = false;

            using (IDbConnection connection = SqlServerHelper.Connection)
                 existe = connection.Query<Int32>("SELECT COUNT(Id) FROM Usuario WHERE LOWER(Login) = LOWER(@login)", new { login })
                    .FirstOrDefault() > 0;

            if (existe)
                throw new UsuarioJaCadastradoException();
        }

        public Usuario FindUserByLogin(String login)
        {
            using (IDbConnection connection = SqlServerHelper.Connection)
                return connection.Query<Usuario>(@"SELECT * FROM Usuario WHERE Login = @login", new { login }).SingleOrDefault();
        }
    }
}
