using ProjectPractice.Services;

namespace ProjectPractice.Tests;

public class GradeValueValidatorTests
{
    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    public void IsValid_AllowedValue_True(int value)
    {
        // Act
        var result = GradeValueValidator.IsValid(value);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData(-2)]
    [InlineData(6)]
    [InlineData(100)]
    public void IsValid_NotAllowedValue_False(int value)
    {
        // Act
        var result = GradeValueValidator.IsValid(value);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void ThrowIfInvalid_NotAllowedValue_ArgumentException()
    {
        // Act + Assert
        Assert.Throws<ArgumentException>(() => GradeValueValidator.ThrowIfInvalid(7));
    }
}
