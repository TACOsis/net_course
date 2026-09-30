using System.Text;

namespace  TodoManager;

public class Program
{
    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var path = "TodoList.json";
        var jsonTodoStorage = new JsonTodoStorage(path);
        TodoService? todoService = null;
        
        try
        { 
            todoService = await TodoService.CreateAsync(jsonTodoStorage);
        }   
        catch (TodoStorageException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("Набор команд:\nadd <заголовок> [--due YYYY-MM-DD]\nlist [--status new|progress|done] [--sort due|created|title]\ndone <идентификатор>\nrm <идентификатор>\nfind <подстрока>\nstats\nhelp\nexit\n");

        while (true)
        {
            if (todoService == null)
            {
                Console.WriteLine("Service stop");
                break;
            }
            
            var line = Console.ReadLine();
            var command = line?.Split(" ");
            var commandName = command?[0];

            if (string.IsNullOrWhiteSpace(commandName))
            {
                Console.WriteLine("Invalid command");
                continue;
            }
            if (line == "exit") break;

            switch (commandName)
            {
                case "add":
                    if (commandName.Length != 3) Console.WriteLine("Invalid command Add");

                    if (command?[1] == null)
                    {
                        Console.WriteLine("Title can't be empty");
                        continue;
                    }
                    
                    var title = command[1];
                    
                    if (!DateOnly.TryParse(command[2], out var date))
                    {
                        Console.WriteLine("Invalid date");
                        continue;
                    }
                    var dueAt = date;

                    Add(todoService, title, dueAt);
                    break;
                default:
                    Console.WriteLine($"Unknown command: {commandName}");
                    break;
            }
            
        }
    }
    public static async void Add(TodoService service, string? title, DateOnly? dueAt)
    {
        await service.AddAsync(title, dueAt);
    }
}