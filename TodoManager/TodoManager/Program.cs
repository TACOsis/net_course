using System.Text;
using System.Text.Json;

namespace  TodoManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var path = "TodoList.json";

        JsonTodoStorage jsonTodoStorage;

        try
        { 
            jsonTodoStorage = new JsonTodoStorage(path);
        }
        catch (JsonException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        var todoService = await TodoService.CreateAsync(jsonTodoStorage);
        await todoService.AddAsync("test", new DateOnly(2026, 7, 25));
        
    }
}