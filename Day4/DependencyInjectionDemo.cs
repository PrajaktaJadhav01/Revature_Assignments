using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace DependencyInjectionDemo
{
    // Program Topic: Dependency Injection & SOLID Principles (DIP)
    public class Program
    {
        public static void Main(string[] args)
        {
            var services = new ServiceCollection();

            services.AddScoped<IMessageReader, TwitterMessageReader>();
            services.AddScoped<IMessageWriter, InstagramMessageWriter>();
            services.AddScoped<IMessageWriter, PdfMessageWriter>();
            services.AddScoped<IMyLogger, ConsoleLogger>();
            services.AddScoped<App>();

            var serviceProvider = services.BuildServiceProvider();

            var app = serviceProvider.GetRequiredService<App>();
            app.Run();
        }
    }

    public class App
    {
        private readonly IMessageReader _messageReader;
        private readonly IEnumerable<IMessageWriter> _messageWriters;

        public App(IMessageReader reader, IEnumerable<IMessageWriter> writers)
        {
            _messageReader = reader;
            _messageWriters = writers;
        }

        public void Run()
        {
            var message = _messageReader.ReadMessage();

            foreach (var writer in _messageWriters)
            {
                writer.WriteMessage(message);
            }
        }
    }

    public interface IMessageReader
    {
        string ReadMessage();
    }

    public class TwitterMessageReader : IMessageReader
    {
        public string ReadMessage() => "Hello, From Twitter!";
    }

    public interface IMessageWriter
    {
        void WriteMessage(string message);
    }

    public interface IMyLogger
    {
        void Log();
    }

    public class ConsoleLogger : IMyLogger
    {
        public void Log()
        {
            Console.WriteLine("Entering Console");
        }
    }

    public class InstagramMessageWriter : IMessageWriter
    {
        private readonly IMyLogger _logger;

        public InstagramMessageWriter(IMyLogger logger)
        {
            _logger = logger;
        }

        public void WriteMessage(string message)
        {
            _logger.Log();
            Console.WriteLine($"{message} posted to Instagram");
        }
    }

    public class PdfMessageWriter : IMessageWriter
    {
        public void WriteMessage(string message)
        {
            Console.WriteLine($"PDF: {message}");
        }
    }
}
