using Polly;
using Polly.Extensions.Http;
using Polly.CircuitBreaker;

var builder = WebApplication.CreateBuilder(args);

// Add HttpClient with Circuit Breaker
builder.Services.AddHttpClient("customer-service")
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30)));

var app = builder.Build();

app.MapGet("/get-customer", async (IHttpClientFactory factory) =>
{
    var client = factory.CreateClient("customer-service");

    try
    {
        var response = await client.GetAsync("http://localhost:5200/api/customer");

        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            return Results.Ok(data);
        }

        return Results.BadRequest("Customer service error");
    }
    catch (BrokenCircuitException)
    {
        return Results.Ok("Circuit breaker activated. Service temporarily unavailable.");
    }
});

app.Run();