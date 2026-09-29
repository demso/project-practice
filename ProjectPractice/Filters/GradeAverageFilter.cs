namespace ProjectPractice.Filters;

// для среднего балла: по предмету в группе, по году
public class GradeAverageFilter
{
    public int? GroupId { get; set; }
    public int? DisciplineId { get; set; }
    public int? Course { get; set; }
}
