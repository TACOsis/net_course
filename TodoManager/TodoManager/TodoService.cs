namespace TodoManager;

public class TodoService
{
    private readonly ITodoStorage _storage;
    private readonly List<TodoItem> _items;

    private TodoService(ITodoStorage storage, List<TodoItem> items)
    {
        _storage = storage;
        _items = items;
    }

    public static async Task<TodoService> CreateAsync(ITodoStorage storage)
    {
        var items = await storage.LoadAsync();
        return new TodoService(storage, items);
    }

    public async Task<TodoItem> AddAsync(string? title, DateOnly? dueDate)
    {
        if (title == null || string.IsNullOrWhiteSpace(title)) throw new ArgumentException("Title cannot be null or whitespace.", nameof(title));
        
        var item = new TodoItem()
        {
            Id = Guid.NewGuid(),
            Title = title,
            Status = TodoStatus.New,
            CreatedAt = DateTimeOffset.UtcNow,
        };
        
        _items.Add(item);
        await _storage.SaveAsync(_items);
        
        return item;
    }
}