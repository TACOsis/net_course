using System;
using Newtonsoft.Json;

class Program
{
    static Dictionary<string, decimal> Catigories =  new Dictionary<string, decimal>();
    static decimal total_sum_catigories = 0; 
    static string popular_catigory = "";
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

        foreach (var catigory in Catigories)
        {
            Console.WriteLine($"{catigory.Key}: {catigory.Value}");
        }

        Console.WriteLine(JsonConvert.SerializeObject(Catigories, Formatting.Indented));
    }

    static void AddCategory(string catigory_name, decimal catigory_value)
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

    static void PrintCatigories()
    {
        Console.WriteLine("Категория      Сумма\n" +
                          "еда          2000.50\n" +
                          "транспорт     450.00\n" +
                          "--------------------\n" +
                          $"Итого        {total_sum_catigories}\n" +
                          $"Больше всего: {popular_catigory}");
    }
}


