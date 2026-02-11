using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace LoggingDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs.txt")
                .CreateLogger();

            // Create Service Collection
            var services = new ServiceCollection();

            services.AddLogging(config =>
            {
                config.ClearProviders();
                config.AddSerilog();
            });

            services.AddScoped<UserService>();

            // Build Service Provider
            var serviceProvider = services.BuildServiceProvider();

            // Get Service
            var userService = serviceProvider.GetRequiredService<UserService>();

            userService.CreateUser("John");

            Log.CloseAndFlush();
        }
    }

    public class UserService
    {
        private readonly ILogger<UserService> _logger;

        public UserService(ILogger<UserService> logger)
        {
            _logger = logger;
        }

        public void CreateUser(string name)
        {
            _logger.LogInformation("Creating user: {Name}", name);

            try
            {
                // Simulating user creation
                _logger.LogInformation("User {Name} created successfully", name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create user: {Name}", name);
            }
        }
    }
}
