namespace ProjectPractice.Dtos;

// плоская модель студента для списков, без вложенных сущностей
public class StudentDto
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public int GroupId { get; set; }
    public string GroupName { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }
}
