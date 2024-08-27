namespace TaskWebApp.DTOs;

public class TaskFilterDto
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string? StatusId { get; set; }
}