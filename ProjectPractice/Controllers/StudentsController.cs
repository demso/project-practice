using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StudentsController(IStudentService studentService) : ControllerBase {
    [HttpPost("list")]
    public async Task<IActionResult> GetStudentsList([FromBody] StudentFilter filter, CancellationToken cancellationToken)
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
        var deleted = await studentService.DeleteStudentAsync(studentId, cancellationToken);
        if (!deleted)
            return NotFound();

        return Ok();
    }
}
