using ProjectPractice.Models;

namespace ProjectPractice.Tests;

public class SpecialtyTests
{
    [Theory]
    [InlineData("09.02.07")]
    [InlineData("1.2.3")]
    [InlineData("38.02.01")]
    public void IsValidCode_ValidCode_True(string code)
    {
        // Act
        var result = Specialty.IsValidCode(code);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("")]            // пустая строка
    [InlineData("09.02")]       // два числа
    [InlineData("09.02.07.01")] // четыре числа
    [InlineData("09-02-07")]    // не тот разделитель
    [InlineData("09.02.07a")]   // буква в конце
    [InlineData("ab.cd.ef")]    // не числа
    [InlineData(" 09.02.07 ")]  // пробелы по краям
    public void IsValidCode_InvalidCode_False(string code)
    {
        // Act
        var result = Specialty.IsValidCode(code);

        // Assert
        Assert.False(result);
    }
}
