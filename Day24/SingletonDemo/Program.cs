using System;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// Register Singleton
services.AddSingleton<DiSingleton>();

// Build Service Provider
var serviceProvider = services.BuildServiceProvider();

// Get Singleton instances
var diLogger1 = serviceProvider.GetRequiredService<DiSingleton>();
var diLogger2 = serviceProvider.GetRequiredService<DiSingleton>();

// Manual instance
var loggerManual = new DiSingleton();

// Print HashCodes
Console.WriteLine("DI Singleton Instance 1: " + diLogger1.GetHashCode());
Console.WriteLine("DI Singleton Instance 2: " + diLogger2.GetHashCode());
Console.WriteLine("Manual Instance: " + loggerManual.GetHashCode());

public class DiSingleton
{
    public int Value { get; set; }
}