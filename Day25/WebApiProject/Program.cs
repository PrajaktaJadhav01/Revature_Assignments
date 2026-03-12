using Microsoft.EntityFrameworkCore;
using WebApiProject.Data;
using WebApiProject.Mapping;
using FluentValidation;
using FluentValidation.AspNetCore;
using WebApiProject.Validators;
using Consul;   // Consul added

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.WriteIndented = true;
    });

// FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

// Add Consul Client
builder.Services.AddSingleton<IConsulClient, ConsulClient>(p =>
    new ConsulClient(config =>
    {
        config.Address = new Uri("http://localhost:8500");
    })
);

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();


// Register service with Consul
var consulClient = app.Services.GetRequiredService<IConsulClient>();

var registration = new AgentServiceRegistration()
{
    ID = "customer-service-1",
    Name = "customer-service",
    Address = "localhost",
    Port = 5200
};

await consulClient.Agent.ServiceRegister(registration);

app.Run();