using JeffersonMello.Livraria.Model.Cadastro.Item;
using JeffersonMello.Livraria.Model.Filter.Cadastro.Item;

namespace JeffersonMello.Livraria.Business.Contract.Cadastro.Item
{
    public interface ILivroBusiness : IBusiness<Livro, LivroFilter>
    {
    }
}