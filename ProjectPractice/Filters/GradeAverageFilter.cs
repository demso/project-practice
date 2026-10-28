namespace ProjectPractice.Filters;

// для среднего балла: по студенту, по предмету в группе, по году
public class GradeAverageFilter
{
    public int? StudentId { get; set; }
    public int? GroupId { get; set; }
    public int? DisciplineId { get; set; }
    public int? Course { get; set; }
}
