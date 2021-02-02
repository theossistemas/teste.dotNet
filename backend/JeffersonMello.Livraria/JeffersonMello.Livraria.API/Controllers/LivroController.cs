using JeffersonMello.Livraria.API.Controllers.Abstract;
using JeffersonMello.Livraria.Business.Contract.Cadastro.Item;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Model.Filter.Cadastro.Item;
using Microsoft.AspNetCore.Mvc;

namespace JeffersonMello.Livraria.API.Controllers
{
    [ApiController, Route("Livro")]
    public class LivroController : ApiControllerBase<Livro, LivroFilter, ILivroBusiness>
    {
    }
}