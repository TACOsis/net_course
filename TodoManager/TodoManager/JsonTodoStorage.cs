using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TodoManager;

public class JsonTodoStorage : ITodoStorage
{
    private readonly string _path;
    private readonly JsonSerializerOptions _optionsJson;

    public JsonTodoStorage(string path)
    {
        _optionsJson = new JsonSerializerOptions()
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        };
        _optionsJson.Converters.Add(new JsonStringEnumConverter());
        
        if (string.IsNullOrEmpty(path)) throw new ArgumentException("Path is empty");
        
        _path = path;
    }
    
    public async Task<List<TodoItem>> LoadAsync()
    {
        if (!File.Exists(_path)) return [];
        
        var fileName = Path.GetFileNameWithoutExtension(_path);
        await using var stream = File.OpenRead(_path);
        List<TodoItem>? items;
        try
        {
            items =  await JsonSerializer.DeserializeAsync<List<TodoItem>>(stream, _optionsJson);
        }
        catch
        {
            var content = await File.ReadAllTextAsync(_path);
            
            if (string.IsNullOrEmpty(content)) return [];
            
            var backup = $"backup/{fileName}.corrupt-{DateTime.UtcNow:yyyyMMdd-HHmmss}";
            
            if (!Directory.Exists("backup")) Directory.CreateDirectory("backup");
            
            File.Move(_path, backup);
            
            throw new TodoStorageException($"Файл с задачами повреждён. Копия сохранена как {backup}\nНачинаю с пустого списка.", backup);
        }
        return items ?? [];
    }

    public async Task SaveAsync(IReadOnlyList<TodoItem> items)
    {
        var itemsJson = JsonSerializer.Serialize(items, _optionsJson);

        if (!File.Exists(_path))
        {
            File.Create(_path).Close();
        }
        
        await File.WriteAllTextAsync(_path, itemsJson);
    }
}