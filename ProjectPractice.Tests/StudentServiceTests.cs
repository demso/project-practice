using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Tests;

public class StudentServiceTests
{
    [Fact]
    public async Task GetStudentsAsync_KT3120_OneObject()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var studentService = new StudentService(ctx);

        var groups = new List<Group>
        {
            new Group { Id = 1, Name = "KT-31-20", Course = 3, SpecialtyId = 1 },
            new Group { Id = 2, Name = "KT-41-20", Course = 4, SpecialtyId = 1 }
        };
        await ctx.Groups.AddRangeAsync(groups);

        var students = new List<Student>
        {
            new Student { FirstName = "Иван", LastName = "Петров", GroupId = 1 },
            new Student { FirstName = "Мария", LastName = "Сидорова", GroupId = 2 }
        };
        await ctx.Students.AddRangeAsync(students);
        await ctx.SaveChangesAsync();

        // Act
        var result = await studentService.GetStudentsAsync(new StudentFilter { GroupName = "KT-31-20" });

        // Assert
        Assert.Single(result);
        Assert.Equal("Петров", result[0].LastName);
    }

    [Fact]
    public async Task GetStudentsAsync_FioSidorova_OneObject()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var studentService = new StudentService(ctx);

        await ctx.Groups.AddAsync(new Group { Id = 1, Name = "KT-31-20", Course = 3, SpecialtyId = 1 });

        var students = new List<Student>
        {
            new Student { FirstName = "Иван", LastName = "Петров", GroupId = 1 },
            new Student { FirstName = "Мария", LastName = "Сидорова", GroupId = 1 }
        };
        await ctx.Students.AddRangeAsync(students);
        await ctx.SaveChangesAsync();

        // Act
        var result = await studentService.GetStudentsAsync(new StudentFilter { Fio = "Сидорова" });

        // Assert
        Assert.Single(result);
        Assert.Equal("Мария", result[0].FirstName);
    }
}
