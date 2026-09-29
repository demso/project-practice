namespace ProjectPractice.Models;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public int GroupId { get; set; }
    public Group? Group { get; set; }

    // навигационное свойство для оценок
    public List<Grade> Grades { get; set; } = new();
}
