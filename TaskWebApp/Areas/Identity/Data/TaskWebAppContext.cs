using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TaskWebApp.Areas.Identity.Data;
using TaskWebApp.Models;

namespace TaskWebApp.Data;

public class TaskWebAppContext : IdentityDbContext<IdentityUser>
{
    public TaskWebAppContext(DbContextOptions<TaskWebAppContext> options)
        : base(options)
    {
    }

    public DbSet<RefreshToken> RefreshToken { get; set; }
    public DbSet<TaskItem> TaskItems { get; set; }
    public DbSet<TaskItemStatus> TaskItemStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TaskItem>()
            .Property(t => t.StatusId)
            .HasConversion<string>();
    }
}
