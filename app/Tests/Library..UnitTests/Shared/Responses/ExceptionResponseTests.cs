using FluentAssertions;
using Library.Shared.Responses;
using System.Net;

namespace Library.UnitTests.Shared.Responses;

public class ExceptionResponseTests
{
    [Fact]
    public void ExceptionResponse_ShouldSetPropertiesCorrectly()
    {
        // Arrange
        var code = HttpStatusCode.BadRequest;
        var message = "Erro de teste";
        var details = new List<string> { "Detalhe 1", "Detalhe 2" };

        // Act
        var response = new ExceptionResponse(code, message, details);

        // Assert
        response.Code.Should().Be(code);
        response.Message.Should().Be(message);
        response.Details.Should().NotBeNull();
        response.Details.Should().BeEquivalentTo(details);
    }

    [Fact]
    public void ExceptionResponse_ShouldAllowNullDetails()
    {
        // Arrange
        var code = HttpStatusCode.InternalServerError;
        var message = "Erro sem detalhes";

        // Act
        var response = new ExceptionResponse(code, message);

        // Assert
        response.Code.Should().Be(code);
        response.Message.Should().Be(message);
        response.Details.Should().BeNull();
    }
}