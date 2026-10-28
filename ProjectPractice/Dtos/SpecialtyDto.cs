namespace ProjectPractice.Dtos;

public class SpecialtyDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
