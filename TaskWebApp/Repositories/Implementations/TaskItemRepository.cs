using Microsoft.EntityFrameworkCore;
using TaskWebApp.Data;
using TaskWebApp.DTOs;
using TaskWebApp.Models;
using TaskWebApp.Repositories.Interfaces;

namespace TaskWebApp.Repositories.Implementations;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly TaskWebAppContext _context;

    public TaskItemRepository(TaskWebAppContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem?>> GetFilteredAsync(string userId, TaskFilterDto filter)
    {
        try
        {
            var query = _context.TaskItems.AsQueryable();

            // Filter by UserId
            query = query.Where(task => task.UserId == userId);

            // Filter by CreatedDate if the date range is provided
            if (filter.StartDate.HasValue && filter.EndDate.HasValue)
            {
                query = query.Where(task => task.CreatedDate >= filter.StartDate.Value && task.CreatedDate <= filter.EndDate.Value);
            }
            else if (filter.StartDate.HasValue)
            {
                query = query.Where(task => task.CreatedDate >= filter.StartDate.Value);
            }
            else if (filter.EndDate.HasValue)
            {
                query = query.Where(task => task.CreatedDate <= filter.EndDate.Value);
            }

            // Filter by Status if provided
            if (filter.Status != null)
            {
                query = query.Where(task => task.Status == filter.Status);
            }

            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetFilteredAsync: {ex.Message}");
            throw;
        }
    }


    public async Task<TaskItem?> GetByIdAsync(string id)
    {
        try
        {
            return await _context.TaskItems.FindAsync(id);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in GetByIdAsync: {ex.Message}");
            throw;
        }
    }

    public async Task<TaskItem> AddAsync(TaskItemDto? taskItem)
    {
        if (taskItem == null)
        {
            throw new ArgumentNullException(nameof(taskItem));
        }

        try
        {
            var task = new TaskItem()
            {
                Id =Guid.NewGuid().ToString(),
                Name = taskItem.Name,
                Description = taskItem.Description,
                AttachmentUrl= taskItem.AttachmentUrl,
                Status = "Todo",
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                UserId = taskItem.UserId
            };
            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in AddAsync: {ex.Message}");
            throw;
        }
    }

    public async Task UpdateAsync(TaskUpdateDto? taskItem, string id)
    {
        if (taskItem == null)
        {
            throw new ArgumentNullException(nameof(taskItem));
        }

        try
        {
            var task = new TaskItem()
            {
                Id = id,
                Status = taskItem.Status,
                UpdatedDate = DateTime.Now
            };
            _context.TaskItems.Update(task);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in UpdateAsync: {ex}");
            throw;
        }
    }

    public async Task DeleteAsync(string id)
    {
        try
        {
            var todoItem = await _context.TaskItems.FindAsync(id);
            if (todoItem != null)
            {
                _context.TaskItems.Remove(todoItem);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error in DeleteAsync: {ex.Message}");
            throw;
        }
    }
}