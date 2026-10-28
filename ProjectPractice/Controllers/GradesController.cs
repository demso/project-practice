using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController(IGradeService gradeService) : ControllerBase {
    [HttpGet("list")]
    public async Task<IActionResult> GetGradesList([FromQuery] GradeFilter filter, CancellationToken cancellationToken)
    {
        var grades = await gradeService.GetGradesAsync(filter, cancellationToken);
        return Ok(grades);
    }

    // средний балл по студенту, по предмету в группе или по году, возвращает просто число
    [HttpGet("average")]
    public async Task<IActionResult> GetAverageGrade([FromQuery] GradeAverageFilter filter, CancellationToken cancellationToken)
    {
        var average = await gradeService.GetAverageGradeAsync(filter, cancellationToken);
        return Ok(average);
    }

    [HttpPost]
    public async Task<IActionResult> AddGrade([FromBody] Grade grade, CancellationToken cancellationToken)
    {
        var result = await gradeService.AddGradeAsync(grade, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGrade([FromBody] Grade grade, CancellationToken cancellationToken)
    {
        var result = await gradeService.UpdateGradeAsync(grade, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
