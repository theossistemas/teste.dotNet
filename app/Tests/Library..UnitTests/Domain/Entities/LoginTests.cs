using FluentAssertions;
using Library.Domain.Entities;
using Library.Domain.Enum;
using Library.Domain.Interfaces;
using Moq;

namespace Library.UnitTests.Domain.Entities;

public class LoginTests
{
    [Fact]
    public void Constructor_ShouldSetProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var userName = "JohnDoe";
        var email = "TEST@EXAMPLE.COM";
        var passwordHash = "hash123";
        var createdAt = DateTime.UtcNow;

        // Act
        var login = new Login(id, userName, email, passwordHash, true, createdAt);

        // Assert
        login.Id.Should().Be(id);
        login.UserName.Should().Be(userName);
        login.Email.Should().Be(email.ToLowerInvariant());
        login.PasswordHash.Should().Be(passwordHash);
        login.Active.Should().BeTrue();
        login.CreatedAt.Should().Be(createdAt);
        login.Role.Should().Be(Roles.Admin);
    }

    [Fact]
    public void Constructor_Simple_ShouldSetProperties()
    {
        // Arrange
        var login = new Login("JohnDoe", "TEST@EXAMPLE.COM", "hash123");

        // Assert
        login.UserName.Should().Be("JohnDoe");
        login.Email.Should().Be("test@example.com");
        login.PasswordHash.Should().Be("hash123");
        login.Role.Should().Be(Roles.Admin);
    }

    [Fact]
    public void Create_WithId_ShouldReturnLoginWithCorrectProperties()
    {
        // Arrange
        var id = Guid.NewGuid();
        var login = Login.Create(id, "JaneDoe", "JANE@EXAMPLE.COM", "hash456", true, DateTime.UtcNow);

        // Assert
        login.Id.Should().Be(id);
        login.UserName.Should().Be("JaneDoe");
        login.Email.Should().Be("jane@example.com");
        login.PasswordHash.Should().Be("hash456");
        login.Active.Should().BeTrue();
        login.Role.Should().Be(Roles.Admin);
    }

    [Fact]
    public void Create_WithoutId_ShouldReturnLoginWithCorrectProperties()
    {
        // Act
        var login = Login.Create("JaneDoe", "JANE@EXAMPLE.COM", "hash456");

        // Assert
        login.UserName.Should().Be("JaneDoe");
        login.Email.Should().Be("jane@example.com");
        login.PasswordHash.Should().Be("hash456");
        login.Role.Should().Be(Roles.Admin);
    }

    [Fact]
    public async Task Insert_ShouldCallRepositoryInsert()
    {
        // Arrange
        var mockRepo = new Mock<IRepository<Login>>();
        var login = new Login("JohnDoe", "test@example.com", "hash123");

        mockRepo.Setup(r => r.Insert(login, It.IsAny<CancellationToken>()))
                .ReturnsAsync(login);

        // Act
        var result = await Login.Insert(login, mockRepo.Object, CancellationToken.None);

        // Assert
        result.Should().Be(login);
        mockRepo.Verify(r => r.Insert(login, It.IsAny<CancellationToken>()), Times.Once);
    }
}