using TaskWebApp.Models;

namespace TaskWebApp.DTOs
{
    public class TaskItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string AttachmentUrl { get; set; }
        public string? UserId { get; set; }
    }
}
