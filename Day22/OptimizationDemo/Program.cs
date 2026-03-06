using Microsoft.EntityFrameworkCore;
using OptimizationDemo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseRouting();

app.MapControllers();

app.Run();