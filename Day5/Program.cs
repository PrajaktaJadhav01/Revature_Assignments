using System;
using System.Collections.Generic;


// -----------------------------
// Compile Time Polymorphism
// -----------------------------
class Calculator
{
    public int Add(int a, int b)
    {
        return a + b;
    }

    public double Add(double a, double b)
    {
        return a + b;
    }

    public int Add(int a, int b, int c)
    {
        return a + b + c;
    }
}


// -----------------------------
// Runtime Polymorphism
// -----------------------------
class Animal
{
    public virtual void Speak()
    {
        Console.WriteLine("Animal makes sound");
    }
}

class Dog : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Dog says Woof");
    }
}

class Cat : Animal
{
    public override void Speak()
    {
        Console.WriteLine("Cat says Meow");
    }
}


// -----------------------------
// Object Class Example
// -----------------------------
class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}, Age: {Age}";
    }
}


// -----------------------------
// Collection Example – List
// -----------------------------
class Product
{
    public string Id { get; set; }
    public string Name { get; set; }

    public override string ToString()
    {
        return $"{Id} - {Name}";
    }
}


// -----------------------------
// Collection Example – Dictionary
// -----------------------------
class GradeBook
{
    private Dictionary<string, List<int>> grades = new();

    public void AddGrade(string name, int grade)
    {
        if (!grades.ContainsKey(name))
        {
            grades[name] = new List<int>();
        }

        grades[name].Add(grade);
    }

    public void Display()
    {
        foreach (var student in grades)
        {
            double avg = 0;
            foreach (var g in student.Value)
            {
                avg += g;
            }
            avg = avg / student.Value.Count;

            Console.WriteLine($"{student.Key} Average: {avg}");
        }
    }
}


// -----------------------------
// MAIN METHOD
// -----------------------------
class Program
{
    static void Main()
    {
        Console.WriteLine("=== Method Overloading ===");
        Calculator calc = new Calculator();
        Console.WriteLine(calc.Add(5, 10));
        Console.WriteLine(calc.Add(2.5, 3.5));
        Console.WriteLine(calc.Add(1, 2, 3));


        Console.WriteLine("\n=== Runtime Polymorphism ===");
        List<Animal> animals = new List<Animal>()
        {
            new Dog(),
            new Cat()
        };

        foreach (var animal in animals)
        {
            animal.Speak();
        }


        Console.WriteLine("\n=== ToString Example ===");
        Person p = new Person { Name = "Prajakta", Age = 22 };
        Console.WriteLine(p);


        Console.WriteLine("\n=== List Collection ===");
        List<Product> products = new List<Product>()
        {
            new Product { Id = "P1", Name = "Laptop" },
            new Product { Id = "P2", Name = "Mouse" }
        };

        foreach (var item in products)
        {
            Console.WriteLine(item);
        }


        Console.WriteLine("\n=== Dictionary Collection ===");
        GradeBook book = new GradeBook();
        book.AddGrade("Prajakta", 90);
        book.AddGrade("Prajakta", 80);
        book.AddGrade("Rahul", 70);
        book.Display();
    }
}
