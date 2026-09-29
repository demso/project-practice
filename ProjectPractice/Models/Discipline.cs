namespace ProjectPractice.Models;

public class Discipline
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public List<Grade> Grades { get; set; } = new();
}
