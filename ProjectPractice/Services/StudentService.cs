using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services;

public interface IStudentService
{
    Task<List<StudentDto>> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default);
    Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default);
    Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default);
    Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);
    Task<int> DeleteStudentsByFioAsync(string lastName, string firstName, CancellationToken cancellationToken = default);
}

public class StudentService : IStudentService
{
    private readonly StudentDbContext _dbContext;

    public StudentService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<StudentDto>> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default)
    {
        var students = _dbContext.Students.AsQueryable();

        // регистр и пробелы по краям не влияют на поиск
        if (!string.IsNullOrWhiteSpace(filter.GroupName))
        {
            var groupName = TextNormalizer.ForSearch(filter.GroupName);
            students = students.Where(s => s.Group!.Name.ToLower() == groupName);
        }

        if (!string.IsNullOrWhiteSpace(filter.Fio))
        {
            var fio = TextNormalizer.ForSearch(filter.Fio);
            students = students.Where(s =>
                s.LastName.ToLower().Contains(fio) || s.FirstName.ToLower().Contains(fio));
        }

        if (filter.IsDeleted.HasValue)
            students = students.Where(s => s.IsDeleted == filter.IsDeleted.Value);

        return await students.Select(s => new StudentDto
        {
            Id = s.Id,
            FirstName = s.FirstName,
            LastName = s.LastName,
            GroupId = s.GroupId,
            GroupName = s.Group!.Name,
            IsDeleted = s.IsDeleted
        }).ToListAsync(cancellationToken);
    }

    public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
    {
        student.FirstName = TextNormalizer.ForStorage(student.FirstName);
        student.LastName = TextNormalizer.ForStorage(student.LastName);

        await _dbContext.Students.AddAsync(student, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return student;
    }

    public async Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id, cancellationToken);
        if (existing == null)
            return null;

        existing.FirstName = TextNormalizer.ForStorage(student.FirstName);
        existing.LastName = TextNormalizer.ForStorage(student.LastName);
        existing.GroupId = student.GroupId;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default)
    {
        var student = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == studentId, cancellationToken);
        if (student == null)
            return false;

        student.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<int> DeleteStudentsByFioAsync(string lastName, string firstName, CancellationToken cancellationToken = default)
    {
        // ищем без учета регистра и пробелов по краям
        var normalizedFN= TextNormalizer.ForSearch(firstName);
        var normalizedLN = TextNormalizer.ForSearch(lastName);
        var students = await _dbContext.Students
            .Where(s => !s.IsDeleted &&
                (s.LastName.ToLower().Contains(normalizedLN) && s.FirstName.ToLower().Contains(normalizedFN)))
            .ToListAsync(cancellationToken);

        foreach (var student in students)
            student.IsDeleted = true;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return students.Count;
    }
}
