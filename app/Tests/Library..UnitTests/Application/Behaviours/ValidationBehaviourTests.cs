using FluentValidation;
using FluentValidation.Results;
using Library.Application.Common.Behaviours;
using MediatR;
using Moq;

namespace Library.UnitTests.Application.Behaviours;

public class ValidationBehaviourTests
{
    public class TestRequest : IRequest<TestResponse> { }
    public class TestResponse { }

    private readonly Mock<IValidator<TestRequest>> _validatorMock;
    private readonly ValidationBehaviour<TestRequest, TestResponse> _behaviour;

    public ValidationBehaviourTests()
    {
        _validatorMock = new Mock<IValidator<TestRequest>>();
        _behaviour = new ValidationBehaviour<TestRequest, TestResponse>(new [] { _validatorMock.Object });
    }

    [Fact]
    public async Task Handle_NoValidationErrors_CallsNext_Success()
    {
        // Arrange
        var request = new TestRequest();
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var nextCalled = false;
        RequestHandlerDelegate<TestResponse> next = (d) =>
        {
            nextCalled = true;
            return Task.FromResult(new TestResponse());
        };

        // Act
        var response = await _behaviour.Handle(request, next, CancellationToken.None);

        // Assert
        Assert.True(nextCalled);
        Assert.NotNull(response);
    }

    [Fact]
    public async Task Handle_WithValidationErrors_ThrowsValidationException()
    {
        // Arrange
        var request = new TestRequest();
        var validationFailures = new List<ValidationFailure> { new ValidationFailure("PropertyName", "Error message") };
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<TestRequest>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(validationFailures));

        // Act and Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            _behaviour.Handle(request, next: (d) => Task.FromResult(new TestResponse()), CancellationToken.None));

        Assert.Contains("PropertyName: Error message Severity: Error", exception.Message);
    }
}