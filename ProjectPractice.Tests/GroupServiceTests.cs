using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Tests;

public class GroupServiceTests
{
    [Fact]
    public async Task DeleteGroupAsync_GroupWithStudents_StudentsAlsoDeleted()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var groupService = new GroupService(ctx);

        var group = new Group { Id = 1, Name = "KT-31-20", Course = 3, SpecialtyId = 1 };
        await ctx.Groups.AddAsync(group);

        var students = new List<Student>
        {
            new Student { FirstName = "Иван", LastName = "Петров", GroupId = 1 },
            new Student { FirstName = "Мария", LastName = "Сидорова", GroupId = 1 }
        };
        await ctx.Students.AddRangeAsync(students);
        await ctx.SaveChangesAsync();

        // Act
        var deleted = await groupService.DeleteGroupAsync(1);

        // Assert
        Assert.True(deleted);
        Assert.True(group.IsDeleted);
        Assert.All(students, s => Assert.True(s.IsDeleted));
    }

    [Fact]
    public async Task DeleteGroupAsync_NotExistingGroup_False()
    {
        // Arrange
        var ctx = TestDbContextFactory.Create();
        var groupService = new GroupService(ctx);

        // Act
        var deleted = await groupService.DeleteGroupAsync(999);

        // Assert
        Assert.False(deleted);
    }
}
