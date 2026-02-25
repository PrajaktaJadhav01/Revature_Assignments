using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

// Run methods
await TaskParallelDemoAsync();
await TaskDemoAsync();
ThreadDemo();


// 1️⃣ Parallel Execution using Task.WhenAll
async Task TaskParallelDemoAsync()
{
    Console.WriteLine("\n----- Parallel Task Demo -----\n");

    using var client = new HttpClient();

    var urls = Enumerable.Range(1, 10)
        .Select(i => $"https://jsonplaceholder.typicode.com/posts/{i}")
        .ToList();

    var stopwatch = Stopwatch.StartNew();

    var downloadTasks = urls.Select(async url =>
    {
        int threadBefore = Thread.CurrentThread.ManagedThreadId;

        string content = await client.GetStringAsync(url);

        int threadAfter = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine($"Thread Before: {threadBefore} downloading {url} ({content.Length} chars) [Thread After {threadAfter}]");

        return content;

    }).ToList();

    string[] results = await Task.WhenAll(downloadTasks);

    stopwatch.Stop();
    Console.WriteLine($"\nTotal time (Parallel): {stopwatch.ElapsedMilliseconds} ms");
}



// 2️⃣ Sequential Async Execution
async Task TaskDemoAsync()
{
    Console.WriteLine("\n----- Sequential Task Demo -----\n");

    using var client = new HttpClient();

    var urls = Enumerable.Range(1, 10)
        .Select(i => $"https://jsonplaceholder.typicode.com/posts/{i}")
        .ToList();

    var stopwatch = Stopwatch.StartNew();

    foreach (var url in urls)
    {
        int threadBefore = Thread.CurrentThread.ManagedThreadId;

        Console.Write($"[Thread {threadBefore}] Fetching {url}... ");

        string content = await client.GetStringAsync(url);

        int threadAfter = Thread.CurrentThread.ManagedThreadId;

        Console.WriteLine($"done ({content.Length} chars) [Thread {threadAfter}]");
    }

    stopwatch.Stop();
    Console.WriteLine($"\nTotal time (Sequential): {stopwatch.ElapsedMilliseconds} ms");
}



// 3️⃣ Traditional Thread Blocking Demo
void ThreadDemo()
{
    Console.WriteLine("\n----- Thread Blocking Demo -----\n");

    using var client = new HttpClient();

    var urls = Enumerable.Range(1, 10)
        .Select(i => $"https://jsonplaceholder.typicode.com/posts/{i}")
        .ToList();

    var stopwatch = Stopwatch.StartNew();

    foreach (var url in urls)
    {
        int threadId = Thread.CurrentThread.ManagedThreadId;

        Console.Write($"[Thread {threadId}] Fetching {url}... ");

        var response = client.GetAsync(url).Result;
        var content = response.Content.ReadAsStringAsync().Result;

        Console.WriteLine($"done ({content.Length} chars)");
    }

    stopwatch.Stop();
    Console.WriteLine($"\nTotal time (Thread Blocking): {stopwatch.ElapsedMilliseconds} ms");
}