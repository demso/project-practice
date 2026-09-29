using Microsoft.EntityFrameworkCore;
using ProjectPractice.Database;
using ProjectPractice.Filters;
using ProjectPractice.Models;

namespace ProjectPractice.Services;

public interface IGroupService
{
    Task<List<Group>> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken = default);
    Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default);
    Task<Group?> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default);
    Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default);
}

public class GroupService : IGroupService
{
    private readonly StudentDbContext _dbContext;

    public GroupService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Group>> GetGroupsAsync(GroupFilter filter, CancellationToken cancellationToken = default)
    {
        var groups = _dbContext.Groups
            .Include(g => g.Specialty)
            .AsQueryable();

        if (!string.IsNullOrEmpty(filter.SpecialtyTitle))
            groups = groups.Where(g => g.Specialty.Title == filter.SpecialtyTitle);

        if (filter.Course.HasValue)
            groups = groups.Where(g => g.Course == filter.Course.Value);

        if (filter.IsDeleted.HasValue)
            groups = groups.Where(g => g.IsDeleted == filter.IsDeleted.Value);

        return await groups.ToListAsync(cancellationToken);
    }

    public async Task<Group> AddGroupAsync(Group group, CancellationToken cancellationToken = default)
    {
        await _dbContext.Groups.AddAsync(group, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return group;
    }

    public async Task<Group?> UpdateGroupAsync(Group group, CancellationToken cancellationToken = default)
    {
        var existing = await _dbContext.Groups.FirstOrDefaultAsync(g => g.Id == group.Id, cancellationToken);
        if (existing == null)
            return null;

        existing.Name = group.Name;
        existing.Course = group.Course;
        existing.SpecialtyId = group.SpecialtyId;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return existing;
    }

    public async Task<bool> DeleteGroupAsync(int groupId, CancellationToken cancellationToken = default)
    {
        var group = await _dbContext.Groups
            .Include(g => g.Students)
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken);
        if (group == null)
            return false;

        // мягкое удаление, студенты группы тоже удаляются
        group.IsDeleted = true;
        foreach (var student in group.Students)
            student.IsDeleted = true;

        await _dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
