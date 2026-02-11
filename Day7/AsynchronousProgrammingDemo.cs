using System;
using System.Threading;
using System.Threading.Tasks;

class AsynchronousProgrammingDemo
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Fetching...");

        // Async call
        var data = await FetchDataAsync();

        Console.WriteLine($"Got: {data}");

        Console.ReadLine();
    }

    static string FetchData()
    {
        Thread.Sleep(5000); // Blocks thread
        return "Data";
    }

    static async Task<string> FetchDataAsync()
    {
        await Task.Delay(5000); // Non-blocking delay
        return "Data";
    }
}
