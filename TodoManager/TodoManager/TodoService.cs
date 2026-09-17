namespace TodoManager;

public class TodoService
{
    private readonly ITodoStorage _todoStorage;

    public TodoService(ITodoStorage todoStorage)
    {
        _todoStorage = todoStorage;
    }
    
    public Task<List<TodoItem>> LoadAsync(string? path = null)
    {
        return _todoStorage.LoadAsync(path);
    }

    public async Task SaveAsync(IReadOnlyList<TodoItem> items)
    {
        await _todoStorage.SaveAsync(items);
    }
}