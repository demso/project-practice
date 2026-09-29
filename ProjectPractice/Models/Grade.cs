namespace ProjectPractice.Models;

// оценка студента по дисциплине, зачеты эмулируем через оценки
public class Grade
{
    public int Id { get; set; }

    // допустимые значения -1 0 1 2 3 4 5
    public int Value { get; set; }

    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public int DisciplineId { get; set; }
    public Discipline? Discipline { get; set; }

    public bool IsDeleted { get; set; }
}
