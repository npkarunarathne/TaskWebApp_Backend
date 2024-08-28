using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskWebApp.Data;
using TaskWebApp.Models;
using TaskWebApp.Repositories.Implementations;
using TaskWebApp.Areas.Identity.Data;

[TestFixture]
public class UserRepositoryTests
{
    private TaskWebAppContext _context;
    private UserRepository _repository;
    private Mock<UserManager<IdentityUser>> _mockUserManager;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<TaskWebAppContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase_UserRepository")
            .Options;

        _context = new TaskWebAppContext(options);

        _mockUserManager = new Mock<UserManager<IdentityUser>>(
            new Mock<IUserStore<IdentityUser>>().Object,
            null, null, null, null, null, null, null, null);

        _repository = new UserRepository(_mockUserManager.Object, _context);

        // Optionally seed your database with initial data for tests
        _context.Users.AddRange(
            new IdentityUser { Id = "user1", UserName = "user1@test.com", Email = "user1@test.com" },
            new IdentityUser { Id = "user2", UserName = "user2@test.com", Email = "user2@test.com" }
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
    public async Task FindByEmailAsync_ReturnsUser_WhenUserExists()
    {
        // Arrange
        var email = "user1@test.com";
        _mockUserManager.Setup(um => um.FindByEmailAsync(email))
            .ReturnsAsync(new IdentityUser { Id = "user1", UserName = "user1@test.com", Email = "user1@test.com" });

        // Act
        var result = await _repository.FindByEmailAsync(email);

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual("user1@test.com", result.Email);
    }

    [Test]
    public async Task GetUserRolesAsync_ReturnsCorrectRoles()
    {
        // Arrange
        var user = new IdentityUser { UserName = "testuser" };
        var roles = new List<string> { "Admin", "User" };
        _mockUserManager.Setup(um => um.GetRolesAsync(user)).ReturnsAsync(roles);

        // Act
        var result = await _repository.GetUserRolesAsync(user);

        // Assert
        Assert.AreEqual(2, result.Count);
        Assert.Contains("Admin", (System.Collections.ICollection?)result);
        Assert.Contains("User", (System.Collections.ICollection?)result);
    }

    [Test]
    public async Task CreateUserAsync_CreatesUserSuccessfully()
    {
        // Arrange
        var user = new IdentityUser { UserName = "newuser@test.com", Email = "newuser@test.com" };
        _mockUserManager.Setup(um => um.CreateAsync(user, "Password123"))
            .ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _repository.CreateUserAsync(user, "Password123");

        // Assert
        Assert.IsTrue(result);
    }


    [Test]
    public async Task CheckPasswordAsync_ReturnsTrue_WhenPasswordIsCorrect()
    {
        // Arrange
        var user = new IdentityUser { UserName = "user1@test.com", Email = "user1@test.com" };
        _mockUserManager.Setup(um => um.CheckPasswordAsync(user, "Password123"))
            .ReturnsAsync(true);

        // Act
        var result = await _repository.CheckPasswordAsync(user, "Password123");

        // Assert
        Assert.IsTrue(result);
    }
}
