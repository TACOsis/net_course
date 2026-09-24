namespace TodoManager;

public interface ITodoStorage
{
    Task<List<TodoItem>> LoadAsync();
    Task SaveAsync(IReadOnlyList<TodoItem> items);
}