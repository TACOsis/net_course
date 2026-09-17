namespace TodoManager;

public interface ITodoStorage
{
    Task<List<TodoItem>> LoadAsync(string? setting = null);
    Task SaveAsync(IReadOnlyList<TodoItem> items);
}