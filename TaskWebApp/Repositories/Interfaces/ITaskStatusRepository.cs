using TaskWebApp.Models;

namespace TaskWebApp.Repositories.Interfaces
{
    public interface ITaskStatusRepository
    {
        Task<IEnumerable<TaskItemStatus>> GetAllAsync();
        Task<TaskItemStatus> GetByIdAsync(string id);
        Task<TaskItemStatus> CreateAsync(TaskItemStatus taskStatus);
        Task<TaskItemStatus> UpdateAsync(TaskItemStatus taskStatus);
        Task DeleteAsync(string id);
    }
}
