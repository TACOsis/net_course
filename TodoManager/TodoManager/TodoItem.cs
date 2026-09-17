namespace TodoManager;

public class TodoItem
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public TodoStatus? Status { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? DueAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
}