using CustomerAppProject;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddScoped<CustomerService>();

var app = builder.Build();

app.MapControllers();

app.Run();