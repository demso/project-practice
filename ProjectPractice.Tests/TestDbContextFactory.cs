using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;

namespace ProjectPractice.Tests;

// контекст на in-memory бд, база уникальная на каждый тест и удаляется сама
public static class TestDbContextFactory
{
    public static StudentDbContext Create()
    {
        var options = new DbContextOptionsBuilder<StudentDbContext>()
            .UseInMemoryDatabase($"TestDb_{Guid.NewGuid()}")
            .Options;

        return new StudentDbContext(options);
    }
}
