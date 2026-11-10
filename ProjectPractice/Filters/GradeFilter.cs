namespace ProjectPractice.Filters;

public class GradeFilter
{
    public int? StudentId { get; set; }

    // по фамилии студента, регистр и пробелы не важны
    public string? StudentLastName { get; set; }
    public int? GroupId { get; set; }
    public int? DisciplineId { get; set; }

    // фильтр по курсу группы студента
    public int? Course { get; set; }
    public bool? IsDeleted { get; set; } = false;
}
