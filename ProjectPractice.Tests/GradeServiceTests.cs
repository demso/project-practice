using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Tests;

public class GradeServiceTests
{
    [Fact]
    public async Task GetAverageGradeAsync_MixedValues_OnlyMarksCounted()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var gradeService = new GradeService(ctx);

        await ctx.Disciplines.AddAsync(new Discipline { Id = 1, Name = "Базы данных" });
        await ctx.Groups.AddAsync(new Group { Id = 1, Name = "KT-31-20", Course = 3, SpecialtyId = 1 });
        await ctx.Students.AddAsync(new Student { Id = 1, FirstName = "Иван", LastName = "Петров", GroupId = 1 });

        var grades = new List<Grade>
        {
            new Grade { Value = 5, StudentId = 1, DisciplineId = 1 },
            new Grade { Value = 4, StudentId = 1, DisciplineId = 1 },
            new Grade { Value = 3, StudentId = 1, DisciplineId = 1 },
            // зачеты и незачеты в средний балл не идут
            new Grade { Value = 1, StudentId = 1, DisciplineId = 1 },
            new Grade { Value = -1, StudentId = 1, DisciplineId = 1 }
        };
        await ctx.Grades.AddRangeAsync(grades);
        await ctx.SaveChangesAsync();

        // Act
        var average = await gradeService.GetAverageGradeAsync(new GradeAverageFilter { GroupId = 1, DisciplineId = 1 });

        // Assert
        Assert.Equal(4.0, average);
    }

    [Fact]
    public async Task GetGradeValuesByStudentLastNameAsync_ReturnsAllValuesCount()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var gradeService = new GradeService(ctx);

        await ctx.Disciplines.AddAsync(new Discipline { Id = 1, Name = "Базы данных" });
        await ctx.Groups.AddAsync(new Group { Id = 1, Name = "KT-31-20", Course = 3, SpecialtyId = 1 });
        await ctx.Students.AddAsync(new Student { Id = 1, FirstName = "Иван", LastName = "Петров", GroupId = 1 });
        await ctx.Students.AddAsync(new Student { Id = 2, FirstName = "Ольга", LastName = "Сидорова", GroupId = 1 });

        var grades = new List<Grade>
        {
            new Grade { Value = 5, StudentId = 1, DisciplineId = 1 },
            new Grade { Value = 4, StudentId = 1, DisciplineId = 1 },
            new Grade { Value = 3, StudentId = 1, DisciplineId = 1 },
            // удаленная оценка не должна попасть в выборку
            new Grade { Value = 2, StudentId = 1, DisciplineId = 1, IsDeleted = true },
            // оценки другого студента не должны попасть в выборку
            new Grade { Value = 5, StudentId = 2, DisciplineId = 1 }
        };
        await ctx.Grades.AddRangeAsync(grades);
        await ctx.SaveChangesAsync();

        // Act
        var result = await gradeService.GetGradeValuesByStudentLastNameAsync("Петров");

        // Assert
        var values = result.Split(',', StringSplitOptions.RemoveEmptyEntries);
        Assert.Equal(3, values.Length);
    }

    [Fact]
    public async Task GetGradeValuesByStudentLastNameAsync_NoGrades_EmptyString()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var gradeService = new GradeService(ctx);

        await ctx.Disciplines.AddAsync(new Discipline { Id = 1, Name = "Базы данных" });
        await ctx.Groups.AddAsync(new Group { Id = 1, Name = "KT-31-20", Course = 3, SpecialtyId = 1 });
        await ctx.Students.AddAsync(new Student { Id = 1, FirstName = "Иван", LastName = "Петров", GroupId = 1 });
        await ctx.SaveChangesAsync();

        // Act
        var result = await gradeService.GetGradeValuesByStudentLastNameAsync("Петров");

        // Assert
        Assert.Equal(string.Empty, result);
    }

    [Fact]
    public async Task AddGradeAsync_NotAllowedValue_ArgumentException()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var gradeService = new GradeService(ctx);

        var grade = new Grade { Value = 6, StudentId = 1, DisciplineId = 1 };

        // Act + Assert
        await Assert.ThrowsAsync<ArgumentException>(() => gradeService.AddGradeAsync(grade));
    }
}
