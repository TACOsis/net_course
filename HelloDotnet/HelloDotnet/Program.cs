using System.Globalization;

namespace HelloDotnet;

class Program
{
    static void Main()
    {
        Dictionary<string, decimal> categories = new Dictionary<string, decimal>();
        
        while (true)
        {
            var line = Console.ReadLine() ?? "";

            if (string.IsNullOrWhiteSpace(line)) break;

            var splitLine = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

            if (splitLine.Length != 2)
            {
                Console.WriteLine("Формат: категория сумма");
                continue;
            }

            var currentCategory = splitLine[0];

            if (!decimal.TryParse(splitLine[1], CultureInfo.InvariantCulture ,out var currentCategoryValue) || currentCategoryValue <= 0)
            {
                Console.WriteLine($"Не понял сумму {splitLine[1]}. Формат: категория сумма");
                continue;
            }

            AddExpense(categories, currentCategory, currentCategoryValue);
        }
        
        var totalSumCategories = CalculateTotal(categories);
        var popularCategory = FindTopCategory(categories) ?? "нет данных";

        PrintCategories(categories, popularCategory, totalSumCategories);
    }

    static void AddExpense(Dictionary<string, decimal> categories, string categoryName, decimal categoryValue)
    {
        if (!categories.TryAdd(categoryName, categoryValue))
        {
            categories[categoryName] += categoryValue;
        }
    }

    private static decimal CalculateTotal(Dictionary<string, decimal> categories)
    {
        decimal totalSumCategories = 0.0m;
        foreach (var category in categories)
        {
            totalSumCategories += category.Value;
            totalSumCategories += category.Value;
        }
        return totalSumCategories; 
    }

    private static string? FindTopCategory(Dictionary<string, decimal> categories)
    {
        var currentPopularCategory = categories.FirstOrDefault().Key;
        
        foreach (var category in categories)
        {
            if (categories[currentPopularCategory] < category.Value) 
            {
                currentPopularCategory = category.Key;
            }
        }

        return currentPopularCategory;
    }

    private static void PrintCategories(Dictionary<string, decimal> categories, string popularCategory, decimal totalSumCategories)
    {
        const int leftColumn = -15;
        const int rightColumn = 10;
        int lineWidthRow = Math.Abs(leftColumn - rightColumn);

        Console.WriteLine($"{"Категория",leftColumn}{"Сумма",rightColumn}");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Key,leftColumn}{category.Value,rightColumn:F2}");
        }
        Console.WriteLine(new string('-', lineWidthRow));
        Console.WriteLine($"{"Итоги:",leftColumn}{totalSumCategories,rightColumn:F2}");
        Console.WriteLine($"{"Больше всего:",leftColumn}{popularCategory,rightColumn}");
    }
}