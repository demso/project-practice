using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services;

public interface IGradeService
{
    Task<List<GradeDto>> GetGradesAsync(GradeFilter filter, CancellationToken cancellationToken = default);
    Task<double?> GetAverageGradeAsync(GradeAverageFilter filter, CancellationToken cancellationToken = default);
    Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default);
    Task<Grade?> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default);
}

public class GradeService : IGradeService
{
    private readonly StudentDbContext _dbContext;

    public GradeService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GradeDto>> GetGradesAsync(GradeFilter filter, CancellationToken cancellationToken = default)
    {
        var grades = _dbContext.Grades.AsQueryable();

        if (filter.StudentId.HasValue)
            grades = grades.Where(g => g.StudentId == filter.StudentId.Value);

        if (filter.GroupId.HasValue)
            grades = grades.Where(g => g.Student.GroupId == filter.GroupId.Value);

        if (filter.DisciplineId.HasValue)
            grades = grades.Where(g => g.DisciplineId == filter.DisciplineId.Value);

        if (filter.Course.HasValue)
            grades = grades.Where(g => g.Student.Group.Course == filter.Course.Value);

        if (filter.IsDeleted.HasValue)
            grades = grades.Where(g => g.IsDeleted == filter.IsDeleted.Value);

        // проекция в плоское дто, без вложенных студентов и дисциплин
        return await grades.Select(g => new GradeDto
        {
            Id = g.Id,
            Value = g.Value,
            StudentId = g.StudentId,
            StudentLastName = g.Student!.LastName,
            StudentFirstName = g.Student.FirstName,
            DisciplineId = g.DisciplineId,
            DisciplineName = g.Discipline!.Name,
            IsDeleted = g.IsDeleted
        }).ToListAsync(cancellationToken);
    }

    public async Task<double?> GetAverageGradeAsync(GradeAverageFilter filter, CancellationToken cancellationToken = default)
    {
        // средний балл считаем только по оценкам 2-5, зачеты и пустышки не берем
        var grades = _dbContext.Grades
            .Where(g => !g.IsDeleted && g.Value >= 2);

        if (filter.StudentId.HasValue)
            grades = grades.Where(g => g.StudentId == filter.StudentId.Value);

        if (filter.GroupId.HasValue)
            grades = grades.Where(g => g.Student.GroupId == filter.GroupId.Value);

        if (filter.DisciplineId.HasValue)
            grades = grades.Where(g => g.DisciplineId == filter.DisciplineId.Value);

        if (filter.Course.HasValue)
            grades = grades.Where(g => g.Student.Group.Course == filter.Course.Value);

        if (!await grades.AnyAsync(cancellationToken))
            return null;

        return await grades.AverageAsync(g => g.Value, cancellationToken);
    }

    public async Task<Grade> AddGradeAsync(Grade grade, CancellationToken cancellationToken = default)
    {
        GradeValueValidator.ThrowIfInvalid(grade.Value);

        await _dbContext.Grades.AddAsync(grade, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return grade;
    }

    public async Task<Grade?> UpdateGradeAsync(Grade grade, CancellationToken cancellationToken = default)
    {
        GradeValueValidator.ThrowIfInvalid(grade.Value);

        var existing = await _dbContext.Grades.FirstOrDefaultAsync(g => g.Id == grade.Id, cancellationToken);
        if (existing == null)
            return null;

        existing.Value = grade.Value;
        existing.StudentId = grade.StudentId;
        existing.DisciplineId = grade.DisciplineId;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return existing;
    }
}
