using System;
using EmployeeDemo;
using ExtensionMethodsDemo;
using Utilities;

namespace MethodsDemo
{
    class ParametersDemo
    {
        public void Configure(int timeout = 30, bool verbose = false)
        {
            Console.WriteLine($"Timeout set to: {timeout} seconds");
            Console.WriteLine($"Verbose mode: {verbose}");
        }

        public void SetCount(out int count)
        {
            count = 42;
        }

        public int ParamsDemo(params int[] numbers)
        {
            int sum = 0;

            foreach (var number in numbers)
            {
                sum += number;
            }

            return sum;
        }

        // Made Logger public so it can be used outside if needed
        public class Logger
        {
            public void Log(string message)
            {
                Console.WriteLine(message);
            }

            public int Log(int message)
            {
                Console.WriteLine(message);
                return message;
            }
        }
    }

    // ---------------- STUDENT CLASS ----------------
    class Student
    {
        public static int NumberOfStudents = 0;

        public string Name { get; set; }
        public int Age { get; set; }

        public Student(string name, int age)
        {
            Name = name;
            Age = age;
            NumberOfStudents++;
        }

        public void Promote()
        {
            Print();
        }

        public void Print()
        {
            Console.WriteLine($"Name: {Name}, Age: {Age}, Student Count: {NumberOfStudents}");
        }

        public int DoubleTheAge(int multiplyBy = 2)
        {
            return Age * multiplyBy;
        }
    }

    // ---------------- CALCULATOR ----------------
    class Calculator
    {
        public int a { get; set; }
        public int b { get; set; }

        public Calculator(int a, int b)
        {
            this.a = a;
            this.b = b;
        }

        // Instance Method
        public int Add()
        {
            return a + b;
        }

        // Static Method
        public static int Add(int a, int b)
        {
            return a + b;
        }
    }

    // ---------------- PROGRAM ----------------
    public class Program
    {
        static void Main(string[] args)
        {
            // Expression Bodied Members Demo
            ExpressionBodiedMembersDemo.Demo demo = new ExpressionBodiedMembersDemo.Demo();

            demo.Add(2, 3);
            demo.Subtract(2, 3);

            // You can uncomment to test
            // StudentDemo();
            // ExtensionMethodDemo();
        }

        static void ExtensionMethodDemo()
        {
            Employee dave = new Employee(1, "Dave", "Smith", 30);

            dave.Print();
            dave.DoubleTheAge();
            dave.Print();
        }

        static void StudentDemo()
        {
            var alice = new Student("Alice", 20);
            alice.Print();

            var bob = new Student("Bob", 22);
            bob.Print();

            var dave = new Student("Dave", 24);
            dave.Print();

            var charlie = new Student("Charlie", 26);
            charlie.Print();
        }
    }
}
