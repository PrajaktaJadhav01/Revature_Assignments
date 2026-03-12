using Consul;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Connect to Consul
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p =>
    new ConsulClient(consulConfig =>
    {
        consulConfig.Address = new Uri("http://localhost:8500");
    }));

var app = builder.Build();

app.MapControllers();

// Register service in Consul
var consulClient = app.Services.GetRequiredService<IConsulClient>();

var registration = new AgentServiceRegistration()
{
    ID = "order-service-1",
    Name = "order-service",
    Address = "localhost",
    Port = 5003
};

await consulClient.Agent.ServiceRegister(registration);

app.Run();