using System.Text;
using System.Text.Json;

namespace  TodoManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var path = "/Users/vryzhov/dotnet_course/TodoManager/TodoManager/obj/Debug/todoList.json";
        
        var jsonTodoStorage = new JsonTodoStorage(path);
        var todoService = await TodoService.CreateAsync(jsonTodoStorage);

        await todoService.AddAsync("Test", new DateOnly(2026, 9, 12));
    }
}