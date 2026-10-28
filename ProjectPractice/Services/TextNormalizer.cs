namespace ProjectPractice.Services;

// нормализация текстовых значений: при записи и при поиске
public static class TextNormalizer
{
    // при записи в бд: тримим и схлопываем повторные пробелы
    public static string ForStorage(string value)
    {
        return string.Join(' ', value.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }

    // при поиске: тримим и приводим к нижнему регистру
    public static string ForSearch(string value)
    {
        return ForStorage(value).ToLower();
    }
}
