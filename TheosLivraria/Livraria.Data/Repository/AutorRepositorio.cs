using Livraria.Data.Context;
using Livraria.Domain.Entidades;
using Livraria.Domain.Interfaces.Repository;
using Microsoft.Extensions.Logging;

namespace Livraria.Data.Repository
{
    public class AutorRepositorio : Repository<Autor>, IAutorRepositorio
    {
        private readonly LivrariaContext _livrariaContext;
        private readonly ILogger<AutorRepositorio> _logger;

        public AutorRepositorio(LivrariaContext livrariaContext, ILogger<AutorRepositorio> logger) : base(livrariaContext, logger)
        {
            _livrariaContext = livrariaContext;
            _logger = logger;
        }
    }
}
