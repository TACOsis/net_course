using System.Text.Encodings.Web;
using System.Text.Json;

namespace TodoManager;

public class JsonTodoStorage : ITodoStorage
{
    private readonly string _path;

    public JsonTodoStorage(string path)
    {
        if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path is empty");
        
        _path = path;
    }
    
    public async Task<List<TodoItem>> LoadAsync()
    {
        var fileName = Path.GetFileNameWithoutExtension(_path);
        await using var stream = File.OpenRead(_path);
        List<TodoItem>? items;
        try
        {
            items =  await JsonSerializer.DeserializeAsync<List<TodoItem>>(stream);
        }
        catch
        {
            var content = await File.ReadAllTextAsync(_path);
            
            if (string.IsNullOrEmpty(content)) return [];
            
            var currentDirectory = Path.GetDirectoryName(_path);
            var backupDirectory= $"{currentDirectory}/backup";
            var backup = $"{backupDirectory}/{fileName}.corrupt-{DateTime.UtcNow:yyyyMMdd-HHmmss}";
            
            if (!Directory.Exists(backupDirectory)) Directory.CreateDirectory(backupDirectory);
            
            File.Move(_path, backup);
            await File.Create(_path).DisposeAsync();
            
            throw new TodoStorageException($"Файл с задачами повреждён. Копия сохранена как {backup}\nНачинаю с пустого списка.", backup);
        }
        return items ?? [];
    }

    public async Task SaveAsync(IReadOnlyList<TodoItem> items)
    {
        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        
        var itemsJson = JsonSerializer.Serialize(items, options);
        
        await File.WriteAllTextAsync(_path, itemsJson);
    }
}