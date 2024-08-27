using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TaskWebApp.DTOs;
using TaskWebApp.Models;
using TaskWebApp.Repositories.Interfaces;

namespace TaskWebApp.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TaskItemsController(ITaskItemRepository repository) : ControllerBase
{
    [HttpPost("filter")]
    public async Task<IActionResult> GetFilteredTasks([FromBody] TaskFilterDto filter)
    {
        var userId = User.FindFirstValue("id");
        var items = await repository.GetFilteredAsync(userId, filter);
        return Ok(items);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var item = await repository.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TaskItemDto? taskItem)
    {
        if (taskItem == null)
        {
            return BadRequest();
        }

        var userId = User.FindFirstValue("id");
        taskItem.UserId = userId;

        await repository.AddAsync(taskItem);
        return CreatedAtAction(nameof(GetById), new { id = taskItem.Id }, taskItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] TaskItemDto taskItem)
    {
        if (id != taskItem.Id)
        {
            return BadRequest();
        }

        var existingItem = await repository.GetByIdAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await repository.UpdateAsync(taskItem);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var existingItem = await repository.GetByIdAsync(id);
        if (existingItem == null)
        {
            return NotFound();
        }

        await repository.DeleteAsync(id);
        return NoContent();
    }
}