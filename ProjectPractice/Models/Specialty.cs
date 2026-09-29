namespace ProjectPractice.Models;

public class Specialty
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    // Навигационное свойство
    public List<Group> Groups { get; set; } = new();
}
