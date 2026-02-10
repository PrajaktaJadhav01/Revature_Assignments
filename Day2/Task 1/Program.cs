using System;
using System.Collections.Generic;
using System.Linq;

public record Person(string Name, int Age);

class Program
{
    static void Main()
    {
        // CSV Data (Name,Age)
        string[] csvLines =
        {
            "Prajakta,22",
            "Rahul,17",
            "Sneha,25",
            "Amit,15"
        };

        List<Person> people = new List<Person>();

        // Convert CSV to List<Person>
        foreach (var line in csvLines)
        {
            var parts = line.Split(',');
            people.Add(new Person(parts[0], int.Parse(parts[1])));
        }

        // Filter adults (Age >= 18)
        var adults = people.Where(p => p is { Age: >= 18 });

        Console.WriteLine("Adults List:");
        foreach (var person in adults)
        {
            Console.WriteLine($"{person.Name} - {person.Age}");
        }
    }
}