using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services;

public interface IStudentService
{
    Task<List<Student>> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default);
    Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default);
    Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default);
    Task<bool> DeleteStudentAsync(int studentId, CancellationToken cancellationToken = default);
}

public class StudentService : IStudentService
{
    private readonly StudentDbContext _dbContext;

    public StudentService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Student>> GetStudentsAsync(StudentFilter filter, CancellationToken cancellationToken = default)
    {
        var students = _dbContext.Students.AsQueryable();

        // регистр при сравнении учитывается
        if (!string.IsNullOrEmpty(filter.GroupName))
            students = students.Where(s => s.Group.Name == filter.GroupName);

        if (!string.IsNullOrEmpty(filter.Fio))
            students = students.Where(s => s.LastName.Contains(filter.Fio) || s.FirstName.Contains(filter.Fio));

        if (filter.IsDeleted.HasValue)
            students = students.Where(s => s.IsDeleted == filter.IsDeleted.Value);

        return await students.ToListAsync(cancellationToken);
    }

    public async Task<Student> AddStudentAsync(Student student, CancellationToken cancellationToken = default)
    {
        await _dbContext.Students.AddAsync(student, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return student;
    }

    public async Task<Student?> UpdateStudentAsync(Student student, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.Students.FirstOrDefaultAsync(s => s.Id == student.Id, cancellationToken);
        if (existing == null)
            return null;

        existing.FirstName = student.FirstName;
        existing.LastName = student.LastName;
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
}
