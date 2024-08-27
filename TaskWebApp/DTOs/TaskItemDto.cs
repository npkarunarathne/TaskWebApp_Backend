using TaskWebApp.Models;

namespace TaskWebApp.DTOs
{
    public class TaskItemDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AttachmentUrl { get; set; }
        public string StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UserId { get; set; }
    }
}
