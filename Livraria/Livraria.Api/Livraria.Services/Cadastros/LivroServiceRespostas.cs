using Livraria.Services.Dto;

namespace Livraria.Services.Cadastros
{
    internal class LivroServiceRespostas
    {
        internal static ResponseDto ResponderLivroAlteradoComSucesso()
        {
            return new ResponseDto
            {
                Sucesso = true,
                Mensagem = "Livro alterado com sucesso!"
            };
        }

        internal static ResponseDto ResponderLivroCadastradoComSucesso()
        {
            return new ResponseDto
            {
                Mensagem = "Livro cadastrado com sucesso!",
                Sucesso = true
            };
        }

        internal static ResponseDto ResponderLivroJaCadastrado()
        {
            return new ResponseDto
            {
                Mensagem = "Livro Já cadastrado. Operação cancelada!",
                Sucesso = false
            };
        }

        internal static ResponseDto ResponderLivroExcluidoComSucesso()
        {
            return new ResponseDto
            {
                Sucesso = true,
                Mensagem = "Livro excluído com sucesso!"
            };
        }

        internal static ResponseDto ResponderLivroNaoEncntrado()
        {
            return new ResponseDto
            {
                Sucesso = false,
                Mensagem = "O livro informado não foi encontrado."
            };
        }
    }
}
