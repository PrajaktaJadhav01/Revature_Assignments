using System;
using System.Collections.Generic;

namespace MYAPP
{
    // ================= CALCULATOR =================

    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        public int Divide(int a, int b) => a / b;
    }

    // ================= ORDER CLASSES =================

    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public interface IEmailSender
    {
        void Send(string email, string message);
    }

    public class Order
    {
        public string Email { get; set; }
    }

    public class OrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine($"Order saved for {order.Email}");
        }
    }

    public class EmailSender : IEmailSender
    {
        public void Send(string email, string message)
        {
            Console.WriteLine($"Email sent to {email}: {message}");
        }
    }

    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailSender _emailSender;

        public OrderService(IOrderRepository repository, IEmailSender emailSender)
        {
            _repository = repository;
            _emailSender = emailSender;
        }

        public void PlaceOrder(Order order)
        {
            _repository.Save(order);
            _emailSender.Send(order.Email, "Order placed successfully!");
        }
    }

    // ================= WEATHER SERVICE =================

    public interface IWeatherService
    {
        IEnumerable<double> GetTemperature(string city);
    }

    public class WeatherService : IWeatherService
    {
        public IEnumerable<double> GetTemperature(string city)
        {
            throw new Exception("City not found");
        }
    }

    public class MockWeatherService : IWeatherService
    {
        public IEnumerable<double> GetTemperature(string city)
        {
            yield return 20;
            yield return 21;
            yield return 22;
            yield return 23;
            yield return 24;
        }
    }

    // ================= MAIN PROGRAM =================

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose Option:");
            Console.WriteLine("1 - Run Calculator Tests");
            Console.WriteLine("2 - Run Order Program");
            Console.WriteLine("3 - Run Weather Service Test");

            string choice = Console.ReadLine();

            if (choice == "1")
            {
                RunCalculatorTests();
            }
            else if (choice == "2")
            {
                RunOrderProgram();
            }
            else if (choice == "3")
            {
                RunWeatherTest();
            }
            else
            {
                Console.WriteLine("Invalid Choice");
            }

            Console.ReadLine();
        }

        static void RunCalculatorTests()
        {
            var calc = new Calculator();

            Console.WriteLine("Running Calculator Tests...\n");

            Console.WriteLine(calc.Add(5, 10) == 15 ? "Add Test Passed" : "Add Test Failed");
            Console.WriteLine(calc.Subtract(10, 5) == 5 ? "Subtract Test Passed" : "Subtract Test Failed");
            Console.WriteLine(calc.Multiply(4, 5) == 20 ? "Multiply Test Passed" : "Multiply Test Failed");
            Console.WriteLine(calc.Divide(20, 4) == 5 ? "Divide Test Passed" : "Divide Test Failed");

            Console.WriteLine("\nCalculator Tests Completed.");
        }

        static void RunOrderProgram()
        {
            var repository = new OrderRepository();
            var emailSender = new EmailSender();
            var orderService = new OrderService(repository, emailSender);

            var order = new Order
            {
                Email = "john.doe@orderscompany.com"
            };

            orderService.PlaceOrder(order);
        }

        static void RunWeatherTest()
        {
            IWeatherService weatherService = new MockWeatherService();

            try
            {
                var temps = weatherService.GetTemperature("Pune");

                Console.WriteLine("Temperatures:");

                foreach (var t in temps)
                {
                    Console.WriteLine(t);
                }

                Console.WriteLine("Weather Test Passed");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("Weather Test Failed");
            }
        }
    }
}