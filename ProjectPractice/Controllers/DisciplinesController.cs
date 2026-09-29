using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisciplinesController : ControllerBase
{
    private readonly IDisciplineService _disciplineService;

    public DisciplinesController(IDisciplineService disciplineService)
    {
        _disciplineService = disciplineService;
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetDisciplinesList([FromBody] DisciplineFilter filter, CancellationToken cancellationToken)
    {
        var disciplines = await _disciplineService.GetDisciplinesAsync(filter, cancellationToken);
        return Ok(disciplines);
    }

    [HttpPost]
    public async Task<IActionResult> AddDiscipline([FromBody] Discipline discipline, CancellationToken cancellationToken)
    {
        var result = await _disciplineService.AddDisciplineAsync(discipline, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscipline([FromBody] Discipline discipline, CancellationToken cancellationToken)
    {
        var result = await _disciplineService.UpdateDisciplineAsync(discipline, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{disciplineId}")]
    public async Task<IActionResult> DeleteDiscipline(int disciplineId, CancellationToken cancellationToken)
    {
        var deleted = await _disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);
        if (!deleted)
            return NotFound();

        return Ok();
    }
}
