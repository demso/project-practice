namespace ProjectPractice.Dtos;

public class DisciplineDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public bool IsHumanitarian { get; set; }
    public bool IsTechnical { get; set; }
    public bool IsDeleted { get; set; }
}
