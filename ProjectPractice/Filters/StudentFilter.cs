namespace ProjectPractice.Filters;

public class StudentFilter
{
    public string? GroupName { get; set; }

    public string? LastName { get; set; }
    public string? FirstName { get; set; }
    public bool? IsDeleted { get; set; } = false;
}
