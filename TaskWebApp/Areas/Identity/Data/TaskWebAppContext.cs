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

    public virtual DbSet<RefreshToken> RefreshToken { get; set; }
    public virtual DbSet<TaskItem> TaskItems { get; set; }
    public virtual DbSet<TaskItemStatus> TaskItemStatus { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<TaskItem>()
            .Property(t => t.Status)
            .HasConversion<string>();

        builder.Entity<TaskItemStatus>().HasData(
               new TaskItemStatus
               {
                   Id = Guid.NewGuid().ToString(),
                   Status = "Todo",
                   DisplayOrder = 1
               },
               new TaskItemStatus
               {
                   Id = Guid.NewGuid().ToString(),
                   Status = "In Progress",
                   DisplayOrder = 2
               },
               new TaskItemStatus
               {
                   Id = Guid.NewGuid().ToString(),
                   Status = "Done",
                   DisplayOrder = 3
               }
           );
    }
}
