using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskWebApp.Data;
using TaskWebApp.DTOs;
using TaskWebApp.Models;
using TaskWebApp.Repositories.Implementations;

[TestFixture]
public class TaskItemRepositoryTests
{
    private TaskWebAppContext _context;
    private TaskItemRepository _repository;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TaskWebAppContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new TaskWebAppContext(options);
        _repository = new TaskItemRepository(_context);

        // Seed the database with initial data
        _context.TaskItems.AddRange(
            new TaskItem
            {
                Id = "1",
                Name = "Task 1",
                Description = "Description 1",
                Status = "Todo",
                CreatedDate = DateTime.Now.AddDays(-2),
                UpdatedDate = DateTime.Now.AddDays(-1),
                UserId = "user1"
            },
            new TaskItem
            {
                Id = "2",
                Name = "Task 2",
                Description = "Description 2",
                Status = "In Progress",
                CreatedDate = DateTime.Now.AddDays(-3),
                UpdatedDate = DateTime.Now.AddDays(-1),
                UserId = "user1"
            }
        );
        _context.SaveChanges();
    }

    [TearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();
        _context.Dispose();
    }

    [Test]
    public async Task GetFilteredAsync_ReturnsFilteredTasks()
    {
        // Arrange
        var filter = new TaskFilterDto
        {
            StartDate = DateTime.Now.AddDays(-3),
            EndDate = DateTime.Now.AddDays(-1),
            Status = "Todo"
        };

        // Act
        var result = await _repository.GetFilteredAsync("user1", filter);

        // Assert
        Assert.AreEqual(1, result.Count());
        Assert.AreEqual("Task 1", result.First().Name);
    }

    [Test]
    public async Task GetByIdAsync_ReturnsTask()
    {
        // Act
        var result = await _repository.GetByIdAsync("1");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("Task 1", result.Name);
    }

    [Test]
    public async Task AddAsync_AddsTask()
    {
        // Arrange
        var taskItemDto = new TaskItemDto
        {
            Name = "New Task",
            Description = "New Task Description",
            AttachmentUrl = null,
            UserId = "user1"
        };

        // Act
        var addedTask = await _repository.AddAsync(taskItemDto);

        // Assert
        Assert.IsNotNull(addedTask);
        Assert.AreEqual("New Task", addedTask.Name);
        Assert.AreEqual("Todo", addedTask.Status);

        // Verify task was added to the database
        var taskInDb = await _context.TaskItems.FindAsync(addedTask.Id);
        Assert.IsNotNull(taskInDb);
    }

    [Test]
    public async Task UpdateAsync_UpdatesTask()
    {
        // Arrange
        var taskUpdateDto = new TaskUpdateDto
        {
            Status = "Done"
        };

        // Act
        await _repository.UpdateAsync(taskUpdateDto, "1");

        // Assert
        var updatedTask = await _context.TaskItems.FindAsync("1");
        Assert.IsNotNull(updatedTask);
        Assert.AreEqual("Done", updatedTask.Status);
    }

    [Test]
    public async Task DeleteAsync_DeletesTask()
    {
        // Act
        await _repository.DeleteAsync("1");

        // Assert
        var deletedTask = await _context.TaskItems.FindAsync("1");
        Assert.IsNull(deletedTask);
    }
}
