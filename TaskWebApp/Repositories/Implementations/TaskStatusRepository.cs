using Microsoft.EntityFrameworkCore;
using TaskWebApp.Data;
using TaskWebApp.Models;
using TaskWebApp.Repositories.Interfaces;

namespace TaskWebApp.Repositories.Implementations
{
   public class TaskStatusRepository : ITaskStatusRepository
    {
        private readonly TaskWebAppContext _context;

        public TaskStatusRepository(TaskWebAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItemStatus>> GetAllAsync()
        {
            return await _context.TaskItemStatus.ToListAsync();
        }

        public async Task<TaskItemStatus> GetByIdAsync(string id)
        {
            return await _context.TaskItemStatus.FindAsync(id);
        }

        public async Task<TaskItemStatus> CreateAsync(TaskItemStatus taskStatus)
        {
            taskStatus.Id = Guid.NewGuid().ToString();
            _context.TaskItemStatus.Add(taskStatus);
            await _context.SaveChangesAsync();
            return taskStatus;
        }

        public async Task<TaskItemStatus> UpdateAsync(TaskItemStatus taskStatus)
        {
            _context.Entry(taskStatus).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return taskStatus;
        }

        public async Task DeleteAsync(string id)
        {
            var taskStatus = await _context.TaskItemStatus.FindAsync(id);
            if (taskStatus != null)
            {
                _context.TaskItemStatus.Remove(taskStatus);
                await _context.SaveChangesAsync();
            }
        }
    }
}
