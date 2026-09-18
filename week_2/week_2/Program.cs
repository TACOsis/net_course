namespace week_2;

class Program
{
    static void Main(string[] args)
    {
        foreach (var VARIABLE in Numbers())
        {
            Console.WriteLine($"принимаю {VARIABLE}");
        }
    }
    
    static IEnumerable<int> Numbers()
    {
        Console.WriteLine("отдаю 1");
        yield return 1;
        Console.WriteLine("отдаю 2");
        yield return 2;
    }
}