using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskWebApp.Models;
using TaskWebApp.Repositories.Interfaces;

namespace TaskWebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskStatusController : ControllerBase
    {
        private readonly ITaskStatusRepository _taskStatusRepository;

        public TaskStatusController(ITaskStatusRepository taskStatusRepository)
        {
            _taskStatusRepository = taskStatusRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskStatus>>> GetTaskStatuses()
        {
            var taskStatuses = await _taskStatusRepository.GetAllAsync();
            return Ok(taskStatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskStatus>> GetTaskStatus(string id)
        {
            var taskStatus = await _taskStatusRepository.GetByIdAsync(id);

            if (taskStatus == null)
            {
                return NotFound();
            }

            return Ok(taskStatus);
        }

        [HttpPost]
        public async Task<ActionResult<TaskStatus>> CreateTaskStatus([FromBody] TaskItemStatus taskStatus)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdTaskStatus = await _taskStatusRepository.CreateAsync(taskStatus);

            return CreatedAtAction(nameof(GetTaskStatus), new { id = createdTaskStatus.Id }, createdTaskStatus);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTaskStatus(string id, [FromBody] TaskItemStatus taskStatus)
        {
            if (id != taskStatus.Id)
            {
                return BadRequest();
            }

            var updatedTaskStatus = await _taskStatusRepository.UpdateAsync(taskStatus);

            return Ok(updatedTaskStatus);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskStatus(string id)
        {
            var taskStatus = await _taskStatusRepository.GetByIdAsync(id);
            if (taskStatus == null)
            {
                return NotFound();
            }

            await _taskStatusRepository.DeleteAsync(id);

            return NoContent();
        }
    }
}
