using Domain.Dtos;
using Domain.Interfaces.Services.CategoriaQuarto;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Api.Services.Test
{
    public class LivroGet : LivroTestes
    {
        private ILivroServices _service;
        private Mock<ILivroServices> _serviceMock;

        [Fact(DisplayName = "Metodo Get Id")]
        public async Task E_Possivel_Executar_Get()
        {
            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.Get(LivroObjectDto.Id)).ReturnsAsync(LivroObjectDto);
            _service = _serviceMock.Object;

            var result = await _service.Get(LivroObjectDto.Id);
            Assert.NotNull(result);
            Assert.True(result.Id == LivroObjectDto.Id);
            Assert.Equal(result.Titulo, LivroObjectDto.Titulo);

            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.Get(It.IsAny<int>())).ReturnsAsync((LivroDto)null);
            _service = _serviceMock.Object;

            var resultNull = await _service.Get(Faker.RandomNumber.Next());
            Assert.Null(resultNull);
        }
        [Fact(DisplayName = "Metodo Get All")]
        public async Task E_Possivel_Executar_GetAll()
        {
            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.GetAll()).ReturnsAsync(ListLivroDto);
            _service = _serviceMock.Object;

            var result = await _service.GetAll();
            Assert.NotNull(result);

            var listVazia = new List<LivroDto>();
            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.GetAll()).ReturnsAsync(listVazia);
            _service = _serviceMock.Object;

            var resultEmpty = await _service.GetAll();
            Assert.Empty(resultEmpty);            
        }
    }
}
