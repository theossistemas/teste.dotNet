using Domain.Interfaces.Services.CategoriaQuarto;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Services.Test
{
    public class LivroPostPut : LivroTestes
    {
        private ILivroServices _service;
        private Mock<ILivroServices> _serviceMock;

        [Fact(DisplayName = "Metodo Post")]
        public async Task E_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.Create(LivroObjectDto)).ReturnsAsync(LivroObjectDto);
            _service = _serviceMock.Object;

            var result = await _service.Create(LivroObjectDto);
            Assert.NotNull(result);
            Assert.True(result.Id == LivroObjectDto.Id);
            Assert.Equal(result.Titulo, LivroObjectDto.Titulo);           
        }
        [Fact(DisplayName = "Metodo Put")]
        public async Task E_Possivel_Executar_Update()
        {
            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.Create(LivroObjectDto)).ReturnsAsync(LivroObjectDto);
            _service = _serviceMock.Object;

            var result = await _service.Create(LivroObjectDto);
            Assert.NotNull(result);
            Assert.True(result.Id == LivroObjectDto.Id);
            Assert.Equal(result.Titulo, LivroObjectDto.Titulo);

            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.Update(LivroObjectDto)).ReturnsAsync(LivroObjectDto);
            _service = _serviceMock.Object;

            var resultUpdate = await _service.Update(LivroObjectDto);
            Assert.NotNull(result);
            Assert.True(result.Id == resultUpdate.Id);            
        }
    }
}
