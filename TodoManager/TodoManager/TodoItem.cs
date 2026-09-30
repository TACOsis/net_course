namespace TodoManager;

public class TodoItem
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public TodoStatus Status { get; set; }
    public required DateTimeOffset CreatedAt { get; set; }
    public DateOnly? DueAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }
}