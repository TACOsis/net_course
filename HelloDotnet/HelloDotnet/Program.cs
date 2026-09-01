using System;
using Newtonsoft.Json;

class Program
{
    public static Dictionary<string, decimal> Catigories =  new Dictionary<string, decimal>();
    static void Main(string[] args)
    {
        while (true)
        {
            string line = Console.ReadLine() ?? "";
            string current_catigory = "";
            decimal current_catigory_value = 0;
            string[] splited_line = [];
            
            if (line == string.Empty) break;

            splited_line = line?.Split(" ") ?? [];

            if (splited_line.Length == 0 || splited_line.Length > 2 || splited_line.Length < 2)
            {
                Console.WriteLine("Формат: категория сумма");
                continue;
            };
            
            current_catigory = splited_line[0];

            if (decimal.TryParse(splited_line[1], out current_catigory_value)) ;
            else
            {
                Console.WriteLine($"Не понял сумму {splited_line[1]}. Формат: категория сумма");
                continue;
            };
            
            AddCategory(current_catigory, current_catigory_value);
        }

        Console.WriteLine(JsonConvert.SerializeObject(Catigories, Formatting.Indented));
    }

    public static void AddCategory(string catigory_name, decimal catigory_value)
    {
        if (Catigories.ContainsKey(catigory_name))
        {
            Catigories[catigory_name] += catigory_value;
        }
        else
        {
            Catigories.Add(catigory_name, catigory_value);
        }
    }
}


