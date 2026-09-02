using System.Globalization;

namespace HelloDotnet;

class Program
{
    private static readonly Dictionary<string, decimal> Categories = new Dictionary<string, decimal>();
    private static decimal _totalSumCategories;
    private static string _popularCategory = "";

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

            if (!decimal.TryParse(splitLine[1], out var currentCategoryValue))
            {
                Console.WriteLine($"Не понял сумму {splitLine[1]}. Формат: категория сумма");
                continue;
            }

            AddCategory(currentCategory, currentCategoryValue);
            CalculateTotalSumCategories();
            CalculatePopularCategories();

        }

        PrintCategories();
    }

    static void AddCategory(string categoryName, decimal categoryValue)
    {
        if (!Categories.TryAdd(categoryName, categoryValue))
        {
            Categories[categoryName] += categoryValue;
        }
    }

    private static void CalculateTotalSumCategories()
    {
        _totalSumCategories = 0;
        foreach (var category in Categories)
        {
            _totalSumCategories += category.Value;
        }
    }

    private static void CalculatePopularCategories()
    {
        var currentPopularCategory = Categories.FirstOrDefault().Key;
        foreach (var category in Categories)
        {
            if (Categories[currentPopularCategory] < category.Value) 
            {
                    currentPopularCategory = category.Key;
            }
        }

        _popularCategory = currentPopularCategory;
        
    }

    private static void PrintCategories()
    {
        Console.WriteLine($"{"Категория",-15}        {"Сумма",10}t");
        foreach (var category in Categories)
        {
            Console.WriteLine($"{category.Key,-15} {category.Value,18:F2}");
        }

        Console.WriteLine(new string('-', 35));
        Console.WriteLine($"{"Итоги:",-15}       {_totalSumCategories,10:F2}");
        Console.WriteLine($"{"Больше всего:",-15}      {_popularCategory,10}");
    }
}