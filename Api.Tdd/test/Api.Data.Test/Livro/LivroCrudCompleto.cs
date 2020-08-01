using Api.Data.Repositories;
using Api.Domain.Entities;
using Data.Context;
using Data.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Api.Data.Test
{
    public class LivroCrudCompleto : BaseTest, IClassFixture<DbTeste>
    {
        private ServiceProvider _serviceProvider;
        public LivroCrudCompleto(DbTeste dbTeste)
        {
            _serviceProvider = dbTeste.serviceProvider;
        }
       
        [Fact(DisplayName = "Crud Livro")]
        [Trait("Crud", "Livro")]
        public async Task E_Possivel_Realizar_Crud_Livro()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new BaseRepository<Livro>(context);
                
                var livro = NovoLivro();
                var result = await _repository.InsertAsync(livro);

                Assert.NotNull(result);
                Assert.Equal(livro.Titulo, result.Titulo);
                Assert.False(result.Id == 0);

                var read = await _repository.SelectAsync(result.Id);
                
                Assert.NotNull(read);
                Assert.False(result.Id != read.Id);

                var update = await _repository.UpdateAsync(result);                
                Assert.NotNull(update);
                Assert.False(update.Id != result.Id);

                var deleteLogico = await _repository.DeleteLogicAsync(result.Id);
                Assert.True(deleteLogico);

                var deleteFisico = await _repository.DeleteFisicoAsync(result.Id);
                Assert.True(deleteFisico);
            }
        }

        [Fact(DisplayName = "Insere Livro e depois lê")]
        [Trait("Read", "Livro")]
        public async Task E_Possivel_Read_Livro()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new BaseRepository<Livro>(context);

                var livro = NovoLivro();
                var result = await _repository.InsertAsync(livro);

                var read = await _repository.SelectAsync(result.Id);

                Assert.NotNull(result);
                Assert.NotNull(read);
                Assert.False(result.Id != read.Id);
            }
        }
        [Fact(DisplayName = "Insere Livro e depois atualiza")]
        [Trait("Update", "Livro")]
        public async Task E_Possivel_Update_Livro()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new BaseRepository<Livro>(context);

                var livro = NovoLivro();
                var result = await _repository.InsertAsync(livro);

                result.Ativo = true;
                result.Autor = Faker.Name.FullName();
                result.Categoria = Faker.Name.First();
                result.DataAlteracao = null;
                result.DataCriacao = Faker.DateOfBirth.Next();
                result.DataLancamento = Faker.DateOfBirth.Next();
                result.Emissora = Faker.Company.Name();
                result.Quantidade = Faker.RandomNumber.Next();
                result.Titulo = Faker.Name.FullName();
                result.Valor = Faker.RandomNumber.Next();

                var update = await _repository.UpdateAsync(result);
                Assert.NotNull(result);
                Assert.NotNull(update);
                Assert.False(update.Id != result.Id);
            }
        }

        [Fact(DisplayName = "Insere Livro e depois delete lógico")]
        [Trait("DeleteLogico", "Livro")]
        public async Task E_Possivel_Delete_Logico_Livro()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new BaseRepository<Livro>(context);

                var livro = NovoLivro();
                var result = await _repository.InsertAsync(livro);
                Assert.NotNull(result);

                var delete = await _repository.DeleteLogicAsync(result.Id);
                Assert.True(delete);
            }
        }

        [Fact(DisplayName = "Insere Livro e depois delete fisico")]
        [Trait("DeleteFisico", "Livro")]
        public async Task E_Possivel_Delete_Fisico_Livro()
        {
            using (var context = _serviceProvider.GetService<MyContext>())
            {
                var _repository = new BaseRepository<Livro>(context);

                var livro = NovoLivro();
                var result = await _repository.InsertAsync(livro);
                Assert.NotNull(result);

                var delete = await _repository.DeleteFisicoAsync(result.Id);
                Assert.True(delete);
            }
        }

        public Livro NovoLivro()
        {
            return new Livro
            {
                Ativo = true,
                Autor = Faker.Name.FullName(),
                Categoria = Faker.Name.First(),
                DataAlteracao = null,
                DataCriacao = Faker.DateOfBirth.Next(),
                DataLancamento = Faker.DateOfBirth.Next(),
                Emissora = Faker.Company.Name(),
                Quantidade = Faker.RandomNumber.Next(),
                Titulo = Faker.Name.FullName(),
                Valor = Faker.RandomNumber.Next()
            };
        }
    }
}
