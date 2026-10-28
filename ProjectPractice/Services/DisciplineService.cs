using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Dtos;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services;

public interface IDisciplineService
{
    Task<List<DisciplineDto>> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default);
    Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default);
    Task<Discipline?> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default);
    Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default);
}

public class DisciplineService : IDisciplineService
{
    private readonly StudentDbContext _dbContext;

    public DisciplineService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<DisciplineDto>> GetDisciplinesAsync(DisciplineFilter filter, CancellationToken cancellationToken = default)
    {
        var disciplines = _dbContext.Disciplines.AsQueryable();

        if (filter.IsHumanitarian.HasValue)
            disciplines = disciplines.Where(d => d.IsHumanitarian == filter.IsHumanitarian.Value);

        if (filter.IsTechnical.HasValue)
            disciplines = disciplines.Where(d => d.IsTechnical == filter.IsTechnical.Value);

        if (filter.IsDeleted.HasValue)
            disciplines = disciplines.Where(d => d.IsDeleted == filter.IsDeleted.Value);

        return await disciplines.Select(d => new DisciplineDto
        {
            Id = d.Id,
            Name = d.Name,
            IsHumanitarian = d.IsHumanitarian,
            IsTechnical = d.IsTechnical,
            IsDeleted = d.IsDeleted
        }).ToListAsync(cancellationToken);
    }

    public async Task<Discipline> AddDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
    {
        discipline.Name = TextNormalizer.ForStorage(discipline.Name);

        await _dbContext.Disciplines.AddAsync(discipline, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return discipline;
    }

    public async Task<Discipline?> UpdateDisciplineAsync(Discipline discipline, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.Disciplines.FirstOrDefaultAsync(d => d.Id == discipline.Id, cancellationToken);
        if (existing == null)
            return null;

        existing.Name = TextNormalizer.ForStorage(discipline.Name);
        existing.IsHumanitarian = discipline.IsHumanitarian;
        existing.IsTechnical = discipline.IsTechnical;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteDisciplineAsync(int disciplineId, CancellationToken cancellationToken = default)
    {
        var discipline = await _dbContext.Disciplines.FirstOrDefaultAsync(d => d.Id == disciplineId, cancellationToken);
        if (discipline == null)
            return false;

        discipline.IsDeleted = true;
        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
