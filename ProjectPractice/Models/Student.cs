using System.Text.RegularExpressions;

namespace ProjectPractice.Models;

public class Student
{
    // имя и фамилия: только буквы и дефис между ними (двойные фамилии), без цифр, пробелов и знаков препинания
    private static readonly Regex NameRegex = new(@"^[a-zA-Zа-яА-ЯёЁ]+(-[a-zA-Zа-яА-ЯёЁ]+)*$", RegexOptions.Compiled);

    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    public int GroupId { get; set; }
    public Group? Group { get; set; }

    // навигационное свойство для оценок
    public List<Grade> Grades { get; set; } = new();

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && NameRegex.IsMatch(name);
    }
}
