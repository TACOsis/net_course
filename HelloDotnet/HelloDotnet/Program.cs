using System.Globalization;

namespace HelloDotnet;

class Program
{
    private static void Main()
    {
        var categories = new Dictionary<string, decimal>();
        
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
                Console.WriteLine($"Не понял сумму {splitLine[1]}. Сумма не может быть отрицательной или состоять из букв.");
                continue;
            }

            AddExpense(categories, currentCategory, currentCategoryValue);
        }

        if (categories.Count == 0)
        {
            Console.WriteLine("нет данных");
            return;
        }
        
        var totalSumCategories = categories.Sum(c => c.Value);
        var popularCategory = categories.MaxBy(c => c.Value).Key;
        var precentCategory = PercentCategories(categories, totalSumCategories);

        PrintCategories(categories, precentCategory, popularCategory, totalSumCategories);
    }

    private static void AddExpense(Dictionary<string, decimal> categories, string categoryName, decimal categoryValue)
    {
        if (!categories.TryAdd(categoryName, categoryValue))
        {
            categories[categoryName] += categoryValue;
        }
    }

    private static IReadOnlyDictionary<string, decimal> PercentCategories(IReadOnlyDictionary<string, decimal> categories, decimal totalSumCategories)
    {
        var precentCategory = new Dictionary<string, decimal>();
        var lastKeyCategory = categories.Last().Key;
        decimal sumPrecentCategories = 0;
        foreach (var category in categories)
        {
            var currentPercent = Math.Round(category.Value / totalSumCategories * 100, 2);
            if (lastKeyCategory == category.Key)
            {
                precentCategory.Add(category.Key, (100 - sumPrecentCategories));
                continue;
            }
            sumPrecentCategories += currentPercent;
            precentCategory.Add(category.Key, currentPercent);
        }
        return precentCategory;
    }
    private static void PrintCategories(IReadOnlyDictionary<string, decimal> categories, IReadOnlyDictionary<string, decimal> precentCategories, string popularCategory, decimal totalSumCategories)
    {
        const int countColumn = 3;
        const int leftColumn = -15;
        const int rightColumn = 10;
        var lineWidthRow = Math.Abs(leftColumn) + (rightColumn * (countColumn - 1));

        Console.WriteLine($"{"Категория",leftColumn}{"Сумма",rightColumn}{"Процент", rightColumn}");
        foreach (var category in categories)
        {
            Console.WriteLine($"{category.Key,leftColumn}{category.Value,rightColumn:F2}{precentCategories[category.Key], rightColumn:F2}%");

        }
        Console.WriteLine(new string('-', lineWidthRow));
        Console.WriteLine($"{"Итого:",leftColumn}{totalSumCategories,rightColumn:F2}");
        Console.WriteLine($"{"Больше всего:",leftColumn}{popularCategory,rightColumn}");
    }
}