using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase {
    [HttpGet("list")]
    public async Task<IActionResult> GetStudentsList([FromQuery] StudentFilter filter, CancellationToken cancellationToken)
    {
        var students = await studentService.GetStudentsAsync(filter, cancellationToken);
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] Student student, CancellationToken cancellationToken)
    {
        var result = await studentService.AddStudentAsync(student, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent([FromBody] Student student, CancellationToken cancellationToken)
    {
        var result = await studentService.UpdateStudentAsync(student, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{studentId}")]
    public async Task<IActionResult> DeleteStudent(int studentId, CancellationToken cancellationToken)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(studentId);
        var deleted = await studentService.DeleteStudentAsync(studentId, cancellationToken);
        if (!deleted)
            return NotFound();

        return Ok();
    }

    // мягкое удаление по фио, возвращает сколько студентов удалили
    [HttpDelete("by-fio")]
    public async Task<IActionResult> DeleteStudentsByFio([FromQuery] string lastName, [FromQuery] string firstName, CancellationToken cancellationToken)
    {
        var count = await studentService.DeleteStudentsByFioAsync(lastName, firstName, cancellationToken);
        if (count == 0)
            return NotFound();

        return Ok(count);
    }
}
