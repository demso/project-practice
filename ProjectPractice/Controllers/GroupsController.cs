using Microsoft.AspNetCore.Mvc;
using ProjectPractice.Filters;
using ProjectPractice.Models;
using ProjectPractice.Services;

namespace ProjectPractice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GroupsController(IGroupService groupService) : ControllerBase {
    [HttpGet("list")]
    public async Task<IActionResult> GetGroupsList([FromQuery] GroupFilter filter, CancellationToken cancellationToken)
    {
        var groups = await groupService.GetGroupsAsync(filter, cancellationToken);
        return Ok(groups);
    }

    [HttpPost]
    public async Task<IActionResult> AddGroup([FromBody] Group group, CancellationToken cancellationToken)
    {
        var result = await groupService.AddGroupAsync(group, cancellationToken);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateGroup([FromBody] Group group, CancellationToken cancellationToken)
    {
        var result = await groupService.UpdateGroupAsync(group, cancellationToken);
        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpDelete("{groupId}")]
    public async Task<IActionResult> DeleteGroup(int groupId, CancellationToken cancellationToken)
    {
        var deleted = await groupService.DeleteGroupAsync(groupId, cancellationToken);
        if (!deleted)
            return NotFound();

        return Ok();
    }
}
