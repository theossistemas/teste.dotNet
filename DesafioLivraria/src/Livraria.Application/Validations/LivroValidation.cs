using Livraria.Application.Dtos;
using Livraria.Application.Interfaces;

namespace Livraria.Application.Validations
{
    public class LivroValidation
    {
        private readonly ILivro _livroService;
        private const string MsgIsbnRepetido = "ISBN já cadastrado";

        public LivroValidation(ILivro livroService)
        {
            _livroService = livroService;
        }

        public async Task<string?> ValidarIsbnUnico(LivroCreateDto livroDto)
        {
            var existing = await _livroService.GetByIsbnAsync(livroDto.Isbn);

            if (existing != null)
                return MsgIsbnRepetido;

            return null;
        }
    }
}
