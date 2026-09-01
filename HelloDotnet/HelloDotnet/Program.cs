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
            
            Add_category(current_catigory, current_catigory_value);
            Calculate_total_sum_catigories();
            Calculate_popular_catigories();
            
        }
        
        PrintCatigories();

        Console.WriteLine(JsonConvert.SerializeObject(Catigories, Formatting.Indented));
    }

    static void Add_category(string catigory_name, decimal catigory_value)
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

    static async Task Calculate_total_sum_catigories()
    {
        total_sum_catigories = 0;
        foreach (var catigory in Catigories)
        {
            total_sum_catigories += catigory.Value;
        }
    }

    static async Task Calculate_popular_catigories()
    {
        string current_popular_catigory = Catigories.First().Key;
        foreach (var catigory in Catigories)
        {
            if (Catigories[current_popular_catigory] < catigory.Value)
            {
                current_popular_catigory = catigory.Key;
            }
            continue;
        }
        popular_catigory = current_popular_catigory;
        
    }

    static void PrintCatigories()
    {
        Console.WriteLine($"{"Категория", -15}        {"Сумма", 10}t" );
        foreach (var catigory in Catigories)
        {
            Console.WriteLine($"{catigory.Key, -15} {catigory.Value, 18:F2}");
        }
        Console.WriteLine(new string('-', 35));
        Console.WriteLine($"{"Итоги:", -15}       {total_sum_catigories, 10:F2}");
        Console.WriteLine($"{"Больше всего:", -15}      {popular_catigory, 10:F2}");
    }
}


