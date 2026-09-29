namespace ProjectPractice.Models;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Course { get; set; }
    public bool IsDeleted { get; set; }

    public int SpecialtyId { get; set; }
    public Specialty Specialty { get; set; } = null!;

    // Навигационное свойство
    public List<Student> Students { get; set; } = new();
}
