using Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Api.Services.Test
{
    public class LivroTestes
    {
        public LivroDto LivroObjectDto;
        public List<LivroDto> ListLivroDto = new List<LivroDto>();
        public LivroTestes()
        {
            LivroObjectDto = NovoLivro();

            for (int i = 0; i < 10; i++)
            {
                ListLivroDto.Add(NovoLivro());
            }
        }
        public LivroDto NovoLivro ()
        {
           return new LivroDto
            {
                Ativo = true,
                Autor = Faker.Name.FullName(),
                Categoria = Faker.Name.First(),
                DataLancamento = Faker.DateOfBirth.Next(),
                Emissora = Faker.Company.Name(),
                Quantidade = Faker.RandomNumber.Next(),
                Titulo = Faker.Name.FullName(),
                Valor = Faker.RandomNumber.Next()
            };
        }
    }
}
