namespace ProjectPractice.Models;

/// <summary>
/// Оценка студента по дисциплине.
/// Зачёты эмулируются через оценки: -1 — незачёт, 0 — нет оценки, 1 — зачёт, 2–5 — оценка.
/// </summary>
public class Grade
{
    public int Id { get; set; }

    /// <summary>Допустимые значения: {-1, 0, 1, 2, 3, 4, 5}.</summary>
    public int Value { get; set; }

    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;

    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; } = null!;

    public bool IsDeleted { get; set; }
}
