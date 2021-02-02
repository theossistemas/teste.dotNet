using JeffersonMello.Livraria.API.Controllers.Abstract;
using JeffersonMello.Livraria.Business.Contract.Cadastro.Item;
using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Model.Filter.Cadastro.Item;
using Microsoft.AspNetCore.Mvc;

namespace JeffersonMello.Livraria.API.Controllers
{
    [ApiController, Route("Categoria")]
    public class CategoriaController : ApiControllerBase<Categoria, CategoriaFilter, ICategoriaBusiness>
    {
    }
}