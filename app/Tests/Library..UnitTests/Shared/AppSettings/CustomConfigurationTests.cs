using FluentAssertions;
using Library.Shared.AppSettings;

namespace Library.UnitTests.Shared.AppSettings;

public class CustomConfigurationTests
{
    [Fact]
    public void ConnectionStrings_ShouldReturnExpectedValues()
    {
        // Act
        var conn = CustomConfiguration.ConnectionStrings;

        // Assert
        conn.Should().NotBeNull();
        conn.DefaultConnection.Should().NotBeNull();
    }

    [Fact]
    public void JWTSettings_ShouldReturnExpectedValues()
    {
        // Act
        var jwt = CustomConfiguration.JWTSettings;

        // Assert
        jwt.Should().NotBeNull();
        jwt.Secret.Should().NotBeNull();
    }
}

// Tipos esperados
public class ConnectionStringsType
{
    public string DefaultConnection { get; set; } = default!;
}

public class JWTSettingsType
{
    public string Secret { get; set; } = default!;
    public int ExpireHours { get; set; }
}