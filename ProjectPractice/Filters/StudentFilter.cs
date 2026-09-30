namespace ProjectPractice.Filters;

public class StudentFilter
{
    public string? GroupName { get; set; }

    // матчится по фамилии или имени
    public string? Fio { get; set; }
    public bool? IsDeleted { get; set; } = false;
}
