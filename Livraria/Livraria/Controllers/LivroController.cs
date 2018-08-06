using System.Data.SqlClient;
using Dapper;
using Livraria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Livraria.Controllers
{
    public class LivroController : Controller
    {
        private IConfiguration Configuracoes;

        public LivroController(IConfiguration config)
        {
            Configuracoes = config;
        }

        // GET: Livro
        public ActionResult Index()
        {
            using (SqlConnection conexaoBD = new SqlConnection(Configuracoes.GetConnectionString("LivrariaConnection")))
            {
                var resultado = conexaoBD.Query<Livro>("SELECT * FROM LIVRO ORDER BY NOME");
                return View(resultado);
            }
        }

        // GET: Livro/Details/5
        public ActionResult Details(int id)
        {
            return BuscarLivro(id);
        }

        // GET: Livro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Livro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Livro livro)
        {
            try
            {
                using (SqlConnection conexaoBD = new SqlConnection(Configuracoes.GetConnectionString("LivrariaConnection")))
                    conexaoBD.Execute(@"INSERT LIVRO(NOME, AUTOR, CATEGORIA) VALUES(@NOME, @AUTOR, @CATEGORIA)", livro);
                               
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Livro/Edit/5
        public ActionResult Edit(int id)
        {
            return BuscarLivro(id);
        }

        // POST: Livro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Livro livro)
        {
            try
            {
                using (SqlConnection conexaoBD = new SqlConnection(Configuracoes.GetConnectionString("LivrariaConnection")))
                    conexaoBD.Execute(@"UPDATE LIVRO SET NOME = @NOME, AUTOR = @AUTOR, CATEGORIA = @CATEGORIA WHERE ID = @ID",
                                        new { NOME = livro.Nome, AUTOR = livro.Autor, CATEGORIA = livro.Categoria, ID = id });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Livro/Delete/5
        public ActionResult Delete(int id)
        {
            return BuscarLivro(id);
        }

        // POST: Livro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Livro livro)
        {
            try
            {
                using (SqlConnection conexaoBD = new SqlConnection(Configuracoes.GetConnectionString("LivrariaConnection")))
                    conexaoBD.Execute(@"DELETE FROM LIVRO WHERE ID = @ID", new { ID = livro.Id });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        private ActionResult BuscarLivro(int id)
        {
            using (SqlConnection conexaoBD = new SqlConnection(Configuracoes.GetConnectionString("LivrariaConnection")))
            {
                var livro = conexaoBD.QueryFirstOrDefault<Livro>(@"SELECT * FROM LIVRO WHERE ID = @ID", new { ID = id });
                return View(livro);
            }
        }
    }
}