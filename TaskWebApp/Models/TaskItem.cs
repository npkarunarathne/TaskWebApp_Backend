using System.ComponentModel.DataAnnotations.Schema;
using TaskWebApp.Areas.Identity.Data;

namespace TaskWebApp.Models;

public class TaskItem
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? AttachmentUrl { get; set; }
    public string Status { get; set; }
    [NotMapped]
    public TaskItemStatus Statuses { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string UserId { get; set; }
    [NotMapped]
    public TaskWebAppUser User { get; set; }

}