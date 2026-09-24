using System.Text;
using System.Text.Json;

namespace  TodoManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var path = "/Users/vryzhov/dotnet_course/TodoManager/TodoManager/obj/Debug/net10.0/TodoList.json";

        JsonTodoStorage jsonTodoStorage;

        try
        { 
            jsonTodoStorage = new JsonTodoStorage(path);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
        var todoService = await TodoService.CreateAsync(jsonTodoStorage);
        
    }
}