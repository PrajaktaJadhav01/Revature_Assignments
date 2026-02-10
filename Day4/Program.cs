using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // ---- Person Demo ----
        Person p1 = new Person("Prajakta", 22);
        Console.WriteLine(p1);

        // ---- BankAccount Demo ----
        BankAccount account = new BankAccount();
        account.Deposit(1000);
        account.Withdraw(1200);
        Console.WriteLine($"Balance: {account.Balance}");
        Console.WriteLine($"Is Overdrawn: {account.IsOverdrawn}");

        // ---- Polymorphism Demo ----
        List<Animal> animals = new List<Animal>
        {
            new Dog(),
            new Cat()
        };

        foreach (var animal in animals)
        {
            animal.Speak();
        }
    }
}
