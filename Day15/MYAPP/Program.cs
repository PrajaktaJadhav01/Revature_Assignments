using System;

namespace MYAPP
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Choose Option:");
            Console.WriteLine("1 - Calculator");
            Console.WriteLine("2 - Order");
            Console.WriteLine("3 - Weather");

            string choice = Console.ReadLine();

            if (choice == "1")
                CalculatorProgram.Run();
            else if (choice == "2")
                OrderProgram.Run();
            else if (choice == "3")
                WeatherProgram.Run();
            else
                Console.WriteLine("Invalid Choice");
        }
    }
}