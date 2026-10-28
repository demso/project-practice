namespace ProjectPractice.Dtos;

public class GradeDto
{
    public int Id { get; set; }
    public int Value { get; set; }
    public int StudentId { get; set; }

    public string StudentLastName { get; set; } = string.Empty;
    public string StudentFirstName { get; set; } = string.Empty;
    public int DisciplineId { get; set; }
    public string DisciplineName { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
