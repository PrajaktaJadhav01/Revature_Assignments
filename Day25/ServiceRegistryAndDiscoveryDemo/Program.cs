using Yarp.ReverseProxy;

var builder = WebApplication.CreateBuilder(args);

// Add Reverse Proxy (API Gateway)
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

// Map the proxy
app.MapReverseProxy();

app.Run();