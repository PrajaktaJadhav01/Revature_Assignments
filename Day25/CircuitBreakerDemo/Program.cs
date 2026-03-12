using Polly;
using Polly.CircuitBreaker;
using Polly.Timeout;

var builder = WebApplication.CreateBuilder(args);

// Add HttpClient
builder.Services.AddHttpClient("customer-service");

// Retry Policy
var retryPolicy = Policy
    .Handle<HttpRequestException>()
    .WaitAndRetryAsync(
        3,
        attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)),
        (exception, timeSpan, retryCount, context) =>
        {
            Console.WriteLine($"Retry {retryCount} after {timeSpan.TotalSeconds} sec due to {exception.Message}");
        });

// Circuit Breaker Policy
var circuitBreakerPolicy = Policy
    .Handle<HttpRequestException>()
    .CircuitBreakerAsync(
        exceptionsAllowedBeforeBreaking: 2,
        durationOfBreak: TimeSpan.FromSeconds(10),
        onBreak: (ex, breakDelay) =>
        {
            Console.WriteLine($"Circuit OPEN for {breakDelay.TotalSeconds} seconds");
        },
        onReset: () =>
        {
            Console.WriteLine("Circuit CLOSED. Service healthy again.");
        },
        onHalfOpen: () =>
        {
            Console.WriteLine("Circuit HALF-OPEN. Testing service...");
        });

var app = builder.Build();

app.MapGet("/call-customer", async (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient("customer-service");

    try
    {
        var result = await retryPolicy.ExecuteAsync(async () =>
        {
            return await circuitBreakerPolicy.ExecuteAsync(async () =>
            {
                Console.WriteLine("Calling Customer Service...");
                var response = await client.GetAsync("http://localhost:5200/api/customer");

                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            });
        });

        return Results.Ok(result);
    }
    catch (BrokenCircuitException)
    {
        return Results.Ok("Circuit is OPEN. Request blocked.");
    }
    catch (Exception ex)
    {
        return Results.Ok($"Request failed: {ex.Message}");
    }
});

app.Run();