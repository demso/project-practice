using ProjectPractice.Models;

namespace ProjectPractice.Tests;

public class StudentTests
{
    [Theory]
    [InlineData("Иван")]
    [InlineData("Петров")]
    [InlineData("Анна-Мария")]        // двойное имя
    [InlineData("Салтыкова-Щедрина")] // двойная фамилия
    [InlineData("John")]
    public void IsValidName_ValidName_True(string name)
    {
        // Act
        var result = Student.IsValidName(name);

        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("")]            // пустая строка
    [InlineData("Иван123")]     // цифры
    [InlineData("Иван Петров")] // пробел
    [InlineData("Иван!")]       // знак препинания
    [InlineData("-Иван")]       // дефис в начале
    [InlineData("Иван-")]       // дефис в конце
    [InlineData("Иван--Петров")]// двойной дефис
    public void IsValidName_InvalidName_False(string name)
    {
        // Act
        var result = Student.IsValidName(name);

        // Assert
        Assert.False(result);
    }
}
