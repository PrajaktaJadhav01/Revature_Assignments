using System;

namespace MYAPP
{
    public class Calculator
    {
        public int Add(int a, int b) => a + b;
        public int Subtract(int a, int b) => a - b;
        public int Multiply(int a, int b) => a * b;
        public int Divide(int a, int b) => a / b;
    }

    public class CalculatorProgram
    {
        public static void Run()
        {
            var calc = new Calculator();

            Console.WriteLine(calc.Add(5, 10) == 15 ? "Add Test Passed" : "Add Test Failed");
            Console.WriteLine(calc.Subtract(10, 5) == 5 ? "Subtract Test Passed" : "Subtract Test Failed");
            Console.WriteLine(calc.Multiply(4, 5) == 20 ? "Multiply Test Passed" : "Multiply Test Failed");
            Console.WriteLine(calc.Divide(20, 4) == 5 ? "Divide Test Passed" : "Divide Test Failed");
        }
    }
}