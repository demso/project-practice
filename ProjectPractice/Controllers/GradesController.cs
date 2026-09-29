using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradesController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetGradesList([FromBody] GradeFilter filter, CancellationToken cancellationToken)
    {
        var grades = await _gradeService.GetGradesAsync(filter, cancellationToken);
        return Ok(grades);
    }

    // средний балл по предмету в группе или по году
    [HttpPost("average")]
    public async Task<IActionResult> GetAverageGrade([FromBody] GradeAverageFilter filter, CancellationToken cancellationToken)
    {
        var average = await _gradeService.GetAverageGradeAsync(filter, cancellationToken);
        return Ok(average);
    }

    [HttpPost]
    public async Task<IActionResult> AddGrade([FromBody] Grade grade, CancellationToken cancellationToken)
    {
        var result = await _gradeService.AddGradeAsync(grade, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGrade([FromBody] Grade grade, CancellationToken cancellationToken)
    {
        var result = await _gradeService.UpdateGradeAsync(grade, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }
}
