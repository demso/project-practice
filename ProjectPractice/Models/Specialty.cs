using System.Text.RegularExpressions;

namespace ProjectPractice.Models;

public class Specialty
{
    // код специальности: три числа через точку, например "09.02.07"
    private static readonly Regex CodeRegex = new(@"^\d+\.\d+\.\d+$", RegexOptions.Compiled);

    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Code { get; set; } = string.Empty;
    public bool IsDeleted { get; set; }

    // навигационное свойство
    public List<Group> Groups { get; set; } = new();

    public static bool IsValidCode(string code)
    {
        return !string.IsNullOrWhiteSpace(code) && CodeRegex.IsMatch(code);
    }
}
