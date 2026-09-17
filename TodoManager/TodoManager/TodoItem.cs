namespace TodoManager;

public class TodoItem
{
    public Guid? Id { get; set; }
    public string? Title { get; set; }
    public TodoStatus? Status { get; set; } = TodoStatus.New;
    public DateTimeOffset? CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    public DateOnly? DueAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; } =  DateTimeOffset.UtcNow;
}