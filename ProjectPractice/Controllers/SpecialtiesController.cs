using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpecialtiesController(ISpecialtyService specialtyService) : ControllerBase {
    [HttpGet("list")]
    public async Task<IActionResult> GetSpecialtiesList([FromQuery] SpecialtyFilter filter, CancellationToken cancellationToken)
    {
        var specialties = await specialtyService.GetSpecialtiesAsync(filter, cancellationToken);
        return Ok(specialties);
    }

    [HttpPost]
    public async Task<IActionResult> AddSpecialty([FromBody] Specialty specialty, CancellationToken cancellationToken)
    {
        var result = await specialtyService.AddSpecialtyAsync(specialty, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSpecialty([FromBody] Specialty specialty, CancellationToken cancellationToken)
    {
        var result = await specialtyService.UpdateSpecialtyAsync(specialty, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{specialtyId}")]
    public async Task<IActionResult> DeleteSpecialty(int specialtyId, CancellationToken cancellationToken)
    {
        var deleted = await specialtyService.DeleteSpecialtyAsync(specialtyId, cancellationToken);
        if (!deleted)
            return NotFound();

        return Ok();
    }
}
