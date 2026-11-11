using System.Text.RegularExpressions;

namespace ProjectPractice.Models;

public class Group
{
    // название группы: две буквы, дефис, две цифры, дефис, две цифры, например "кт-31-23"
    private static readonly Regex NameRegex = new(@"^[a-zA-Zа-яА-ЯёЁ]{2}-\d{2}-\d{2}$", RegexOptions.Compiled);

    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Course { get; set; }
    public bool IsDeleted { get; set; }

    public int SpecialtyId { get; set; }
    public Specialty? Specialty { get; set; }

    // навигационное свойство
    public List<Student> Students { get; set; } = new();

    public static bool IsValidName(string name)
    {
        return !string.IsNullOrWhiteSpace(name) && NameRegex.IsMatch(name);
    }
}
