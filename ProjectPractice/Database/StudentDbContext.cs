using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database.Configurations;
using ProjectPractice.Models;

namespace ProjectPractice.Database;

public class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options) {

    DbSet<Student> Students { get; set; }
    DbSet<Group> Groups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentDbContext).Assembly);
    }
}
