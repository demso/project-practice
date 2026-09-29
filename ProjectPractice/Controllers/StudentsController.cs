using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentsController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpPost("list")]
    public async Task<IActionResult> GetStudentsList([FromBody] StudentFilter filter, CancellationToken cancellationToken)
    {
        var students = await _studentService.GetStudentsAsync(filter, cancellationToken);
        return Ok(students);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] Student student, CancellationToken cancellationToken)
    {
        var result = await _studentService.AddStudentAsync(student, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateStudent([FromBody] Student student, CancellationToken cancellationToken)
    {
        var result = await _studentService.UpdateStudentAsync(student, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{studentId}")]
    public async Task<IActionResult> DeleteStudent(int studentId, CancellationToken cancellationToken)
    {
        var deleted = await _studentService.DeleteStudentAsync(studentId, cancellationToken);
        if (!deleted)
            return NotFound();

        return Ok();
    }
}
