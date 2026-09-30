namespace ProjectPractice.Services;

// проверка значения оценки: -1 незачет, 0 нет оценки, 1 зачет, 2-5 оценка
public static class GradeValueValidator
{
    public static bool IsValid(int value)
    {
        return value >= -1 && value <= 5;
    }

    public static void ThrowIfInvalid(int value)
    {
        if (!IsValid(value))
            throw new ArgumentException($"Недопустимое значение оценки: {value}");
    }
}
