namespace ProjectPractice.Dtos;

public class GroupDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Course { get; set; }
    public int SpecialtyId { get; set; }
    public string SpecialtyTitle { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
