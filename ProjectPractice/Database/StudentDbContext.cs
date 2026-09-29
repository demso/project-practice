using Microsoft.EntityFrameworkCore;
using ProjectPractice.Models;

namespace ProjectPractice.Database;

public class StudentDbContext(DbContextOptions<StudentDbContext> options) : DbContext(options) {
    public DbSet<Specialty> Specialties => Set<Specialty>();
    public DbSet<Group> Groups => Set<Group>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Discipline> Disciplines => Set<Discipline>();
    public DbSet<Grade> Grades => Set<Grade>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Каждая таблица конфигурируется отдельно и явно (Fluent API)
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentDbContext).Assembly);
    }
}
