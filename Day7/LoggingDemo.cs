using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace LoggingDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .CreateLogger();

            // Create Service Collection
            var services = new ServiceCollection();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddSerilog(Log.Logger);
            });

            // Register Services
            services.AddScoped<UserService>();

            // Build Service Provider
            var serviceProvider = services.BuildServiceProvider();

            // Get Service
            var userService = serviceProvider.GetRequiredService<UserService>();

            userService.CreateUser("John");

            Log.CloseAndFlush();

            Console.ReadLine(); // Keeps console open
        }
    }

    // ---------------- USER SERVICE ----------------
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
                // Simulate user creation
                _logger.LogInformation("User {Name} created successfully", name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create user: {Name}", name);
            }
        }
    }
}
