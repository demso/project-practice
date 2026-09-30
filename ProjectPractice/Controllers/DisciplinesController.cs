using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DisciplinesController(IDisciplineService disciplineService) : ControllerBase {
    [HttpPost("list")]
    public async Task<IActionResult> GetDisciplinesList([FromBody] DisciplineFilter filter, CancellationToken cancellationToken)
    {
        var disciplines = await disciplineService.GetDisciplinesAsync(filter, cancellationToken);
        return Ok(disciplines);
    }

    [HttpPost]
    public async Task<IActionResult> AddDiscipline([FromBody] Discipline discipline, CancellationToken cancellationToken)
    {
        var result = await disciplineService.AddDisciplineAsync(discipline, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDiscipline([FromBody] Discipline discipline, CancellationToken cancellationToken)
    {
        var result = await disciplineService.UpdateDisciplineAsync(discipline, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{disciplineId}")]
    public async Task<IActionResult> DeleteDiscipline(int disciplineId, CancellationToken cancellationToken)
    {
        var deleted = await disciplineService.DeleteDisciplineAsync(disciplineId, cancellationToken);
        if (!deleted)
            return NotFound();

        return Ok();
    }
}
