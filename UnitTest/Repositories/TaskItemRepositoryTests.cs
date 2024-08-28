using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskWebApp.Data;

namespace UnitTest.Repositories
{
    [TestFixture]
    public class TaskItemRepositoryTests
    {
        private Mock<TaskWebAppContext> _mockContext;
        private TaskItemRepository _repository;
        private List<TaskItem> _taskItems;

        [SetUp]
        public void Setup()
        {
            _taskItems = new List<TaskItem>
            {
                new TaskItem
                {
                    Id = "1", Name = "Task 1", Description = "Description 1", Status = "Todo",
                    CreatedDate = DateTime.Now.AddDays(-2), UpdatedDate = DateTime.Now, UserId = "user1"
                },
                new TaskItem
                {
                    Id = "2", Name = "Task 2", Description = "Description 2", Status = "InProgress",
                    CreatedDate = DateTime.Now.AddDays(-1), UpdatedDate = DateTime.Now, UserId = "user1"
                }
            };

            var mockDbSet = new Mock<DbSet<TaskItem>>();
            mockDbSet.As<IQueryable<TaskItem>>().Setup(m => m.Provider).Returns(_taskItems.AsQueryable().Provider);
            mockDbSet.As<IQueryable<TaskItem>>().Setup(m => m.Expression).Returns(_taskItems.AsQueryable().Expression);
            mockDbSet.As<IQueryable<TaskItem>>().Setup(m => m.ElementType).Returns(_taskItems.AsQueryable().ElementType);
            mockDbSet.As<IQueryable<TaskItem>>().Setup(m => m.GetEnumerator()).Returns(_taskItems.AsQueryable().GetEnumerator());

            _mockContext = new Mock<TaskWebAppContext>();
            _mockContext.Setup(c => c.TaskItems).Returns(mockDbSet.Object);

            _repository = new TaskItemRepository(_mockContext.Object);
        }

        [Test]
        public async Task GetFilteredAsync_FiltersByUserId()
        {
            // Arrange
            var filter = new TaskFilterDto();

            // Act
            var result = await _repository.GetFilteredAsync("user1", filter);

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetByIdAsync_ReturnsCorrectTask()
        {
            // Act
            var result = await _repository.GetByIdAsync("1");

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Task 1", result.Name);
        }

        [Test]
        public async Task AddAsync_AddsTask()
        {
            // Arrange
            var taskDto = new TaskItemDto
            {
                Name = "New Task",
                Description = "New Description",
                UserId = "user2"
            };

            // Act
            var result = await _repository.AddAsync(taskDto);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("New Task", result.Name);
            _mockContext.Verify(c => c.TaskItems.Add(It.IsAny<TaskItem>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task UpdateAsync_UpdatesTask()
        {
            // Arrange
            var updateDto = new TaskUpdateDto
            {
                Status = "Done"
            };

            // Act
            await _repository.UpdateAsync(updateDto, "1");

            // Assert
            var updatedTask = _taskItems.First(t => t.Id == "1");
            Assert.AreEqual("Done", updatedTask.Status);
            _mockContext.Verify(c => c.TaskItems.Update(It.IsAny<TaskItem>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }

        [Test]
        public async Task DeleteAsync_DeletesTask()
        {
            // Act
            await _repository.DeleteAsync("1");

            // Assert
            Assert.AreEqual(1, _taskItems.Count);
            _mockContext.Verify(c => c.TaskItems.Remove(It.IsAny<TaskItem>()), Times.Once);
            _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
        }
    }
}
