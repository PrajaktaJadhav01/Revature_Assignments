using CustomerAppProject;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddScoped<CustomerService>();

var app = builder.Build();

app.UseRouting();
app.MapControllers();

app.Run();