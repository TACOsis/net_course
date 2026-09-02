using System.Globalization;

namespace HelloDotnet;

class Program
{
    private static readonly Dictionary<string, decimal> Categories = new Dictionary<string, decimal>();

    static void Main()
    {
        CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("ru-RU");
        
        while (true)
        {
            var line = Console.ReadLine() ?? "";

            if (line == string.Empty) break;

            var splitLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (splitLine.Length != 2)
            {
                Console.WriteLine("Формат: категория сумма");
                continue;
            }

            var currentCategory = splitLine[0];

            if (!decimal.TryParse(splitLine[1], CultureInfo.DefaultThreadCurrentCulture ,out var currentCategoryValue))
            {
                Console.WriteLine($"Не понял сумму {splitLine[1]}. Формат: категория сумма");
                continue;
            }

            AddCategory(currentCategory, currentCategoryValue);
        }
        
        var totalSumCategories = CalculateTotalSumCategories();
        var popularCategory =  CalculatePopularCategories();
        
        PrintCategories(popularCategory, totalSumCategories);
    }

    static void AddCategory(string categoryName, decimal categoryValue)
    {
        if (!Categories.TryAdd(categoryName, categoryValue))
        {
            Categories[categoryName] += categoryValue;
        }
    }

    private static decimal CalculateTotalSumCategories()
    {
        decimal totalSumCategories = 0.0m;
        foreach (var category in Categories)
        {
            totalSumCategories += category.Value;
        }
        
        return totalSumCategories; 
    }

    private static string CalculatePopularCategories()
    {
        var currentPopularCategory = Categories.FirstOrDefault().Key;
        foreach (var category in Categories)
        {
            if (Categories[currentPopularCategory] < category.Value) 
            {
                    currentPopularCategory = category.Key;
            }
        }

        return currentPopularCategory;
        
    }

    private static void PrintCategories(string popularCategory, decimal totalSumCategories)
    {
        Console.WriteLine($"{"Категория",-15}{"Сумма",10}");
        foreach (var category in Categories)
        {
            Console.WriteLine($"{category.Key,-15}{category.Value,10:F2}");
        }

        Console.WriteLine(new string('-', 25));
        Console.WriteLine($"{"Итоги:",-15}{totalSumCategories,10:F2}");
        Console.WriteLine($"{"Больше всего:",-15}{popularCategory,10}");
    }
}