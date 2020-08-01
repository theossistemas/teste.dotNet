using Domain.Interfaces.Services.CategoriaQuarto;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Api.Services.Test
{
    public class LivroDelete : LivroTestes
    {
        private ILivroServices _service;
        private Mock<ILivroServices> _serviceMock;

        [Fact(DisplayName = "Metodo Post")]
        public async Task E_Possivel_Executar_Create()
        {
            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.Delete(LivroObjectDto.Id)).ReturnsAsync(true);
            _service = _serviceMock.Object;

            var result = await _service.Delete(LivroObjectDto.Id);
            Assert.True(result);

            _serviceMock = new Mock<ILivroServices>();
            _serviceMock.Setup(c => c.Delete(It.IsAny<int>())).ReturnsAsync(false);
            _service = _serviceMock.Object;

            result = await _service.Delete(Faker.RandomNumber.Next());
            Assert.False(result);
        }
    }
}
