using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using Dapper;
using Livraria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Livraria.Controllers
{
    public class AutenticacaoController : Controller
    {
        private IConfiguration Configuracoes;

        public AutenticacaoController(IConfiguration config)
        {
            Configuracoes = config;
        }

        // GET: Autenticacao/Create
        public ActionResult Acessar()
        {
            return View();
        }

        // POST: Autenticacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Acessar(Autenticacao autenticacao)
        {
            try
            {
                string senha = CriptografarSenha(autenticacao.Senha);
                using (SqlConnection conexaoBD = new SqlConnection(Configuracoes.GetConnectionString("LivrariaConnection")))
                {
                    var autent = conexaoBD.QueryFirstOrDefault<Autenticacao>(@"SELECT * FROM AUTENTICACAO WHERE LOGIN = @LOGIN", new { LOGIN = autenticacao.Login});
                    if (autenticacao.Login == autent.Login && senha == autent.Senha)
                        return RedirectToAction(@"../Livro/Index");
                    else
                        return View();
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Autenticacao/Create
        public ActionResult CriarLogin()
        {
            return View();
        }

        // POST: Autenticacao/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CriarLogin(Autenticacao autenticacao)
        {
            try
            {
                string senha = CriptografarSenha(autenticacao.Senha);
                using (SqlConnection conexaoBD = new SqlConnection(Configuracoes.GetConnectionString("LivrariaConnection")))
                    conexaoBD.Execute(@"INSERT AUTENTICACAO(LOGIN, SENHA) VALUES(@LOGIN, @SENHA)", new { LOGIN = autenticacao.Login, SENHA = senha});

                return RedirectToAction("Acessar");
            }
            catch
            {
                return View();
            }
        }

        private string CriptografarSenha(string senha)
        {
            MD5 md5 = MD5.Create();
            byte[] bytes = Encoding.ASCII.GetBytes(senha);
            byte[] hash = md5.ComputeHash(bytes);
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
                stringBuilder.Append(hash[i].ToString("X2"));
            
            return stringBuilder.ToString();
        }

    }
}