using FluentAssertions;
using Library.CrossCutting.Auth.Models;

namespace Library.UnitTests.CrossCutting.Auth.Models;

public class LoginModelTests
{
    [Fact]
    public void Constructor_ShouldInitializePropertiesWithDefaults()
    {
        // Arrange & Act
        var model = new LoginModel();

        // Assert
        model.UserName.Should().BeNullOrEmpty();
        model.Email.Should().BeNullOrEmpty();
        model.Role.Should().BeNullOrEmpty();
    }

    [Fact]
    public void ShouldAllowSettingAndGettingProperties()
    {
        // Arrange
        var model = new LoginModel();

        // Act
        model.UserName = "johndoe";
        model.Email = "johndoe@example.com";
        model.Role = "Admin";

        // Assert
        model.UserName.Should().Be("johndoe");
        model.Email.Should().Be("johndoe@example.com");
        model.Role.Should().Be("Admin");
    }
}