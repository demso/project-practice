using ProjectPractice.Models;

namespace ProjectPractice.Tests;

public class GroupTests
{
    [Theory]
    [InlineData("кт-31-23")]
    [InlineData("КТ-31-23")]
    [InlineData("KT-31-20")]
    public void IsValidName_ValidName_True(string name)
    {
        // Act
        var result = Group.IsValidName(name);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("")]           // пустая строка
    [InlineData("кт31-23")]    // нет первого дефиса
    [InlineData("кт-3123")]    // нет второго дефиса
    [InlineData("кт-31")]      // только две части
    [InlineData("кт-31-23-1")] // лишняя часть
    [InlineData("кт1-31-23")]  // цифра в буквенной части
    [InlineData("кт-3-23")]    // одна цифра
    [InlineData("кт-31-233")]  // три цифры
    [InlineData("кт_31_23")]   // не тот разделитель
    [InlineData("КТ-31-23 ")]
    public void IsValidName_InvalidName_False(string name)
    {
        // Act
        var result = Group.IsValidName(name);

        // Assert
        Assert.False(result);
    }
}
