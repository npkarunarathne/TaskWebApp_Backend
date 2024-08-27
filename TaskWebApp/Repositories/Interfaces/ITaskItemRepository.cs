using TaskWebApp.DTOs;
using TaskWebApp.Models;

namespace TaskWebApp.Repositories.Interfaces;

public interface ITaskItemRepository
{
    Task<IEnumerable<TaskItem?>> GetFilteredAsync(string userId, TaskFilterDto filter);
    Task<TaskItem?> GetByIdAsync(string id);
    Task<TaskItem> AddAsync(TaskItemDto? taskItem);
    Task UpdateAsync(TaskUpdateDto? taskItem,string id);
    Task DeleteAsync(string id);
}