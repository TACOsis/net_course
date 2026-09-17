using System.IO;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace TodoManager;

public class JsonTodoStorage : ITodoStorage
{
    private readonly string _path;

    public JsonTodoStorage(string path)
    {
        _path = path;
    }
    
    public async Task<List<TodoItem>> LoadAsync(string? setting = null)
    { 
        if (string.IsNullOrEmpty(_path)) return [];

        if (string.IsNullOrWhiteSpace(_path) || !File.Exists(_path)) return [];
        
        await using var stream = File.OpenRead(_path);
        
        var items = JsonSerializer.Deserialize<List<TodoItem>>(stream);

        return items ?? [];
    }

    public async Task SaveAsync(IReadOnlyList<TodoItem> items)
    {
        if (string.IsNullOrEmpty(_path) || string.IsNullOrWhiteSpace(_path) || !File.Exists(_path))
        {
            throw new Exception("Path is empty");
        }
        
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        
        var itemsJson = JsonSerializer.Serialize<IReadOnlyList<TodoItem>>(items, options);
        
        await File.WriteAllTextAsync(_path, itemsJson);
    }
}