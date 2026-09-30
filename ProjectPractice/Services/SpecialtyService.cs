using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services;

public interface ISpecialtyService
{
    Task<List<Specialty>> GetSpecialtiesAsync(SpecialtyFilter filter, CancellationToken cancellationToken = default);
    Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default);
    Task<Specialty?> UpdateSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default);
    Task<bool> DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken = default);
}

public class SpecialtyService : ISpecialtyService
{
    private readonly StudentDbContext _dbContext;

    public SpecialtyService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Specialty>> GetSpecialtiesAsync(SpecialtyFilter filter, CancellationToken cancellationToken = default)
    {
        var specialties = _dbContext.Specialties.AsQueryable();

        if (!string.IsNullOrEmpty(filter.Title))
            specialties = specialties.Where(s => s.Title.Contains(filter.Title));

        if (!string.IsNullOrEmpty(filter.Code))
            specialties = specialties.Where(s => s.Code == filter.Code);

        if (filter.IsDeleted.HasValue)
            specialties = specialties.Where(s => s.IsDeleted == filter.IsDeleted.Value);

        return await specialties.ToListAsync(cancellationToken);
    }

    public async Task<Specialty> AddSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default)
    {
        await _dbContext.Specialties.AddAsync(specialty, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return specialty;
    }

    public async Task<Specialty?> UpdateSpecialtyAsync(Specialty specialty, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.Specialties.FirstOrDefaultAsync(s => s.Id == specialty.Id, cancellationToken);
        if (existing == null)
            return null;

        existing.Title = specialty.Title;
        existing.Code = specialty.Code;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteSpecialtyAsync(int specialtyId, CancellationToken cancellationToken = default)
    {
        var specialty = await _dbContext.Specialties
            .Include(s => s.Groups)
            .ThenInclude(g => g.Students)
            .FirstOrDefaultAsync(s => s.Id == specialtyId, cancellationToken);
        if (specialty == null)
            return false;

        // мягкое удаление, каскадно удаляются группы и их студенты
        specialty.IsDeleted = true;
        foreach (var group in specialty.Groups)
        {
            group.IsDeleted = true;
            foreach (var student in group.Students)
                student.IsDeleted = true;
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
