using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

var context = new CustomerManagementContext();

Console.WriteLine("Checking database connection...\n");

try
{
    if (context.Database.CanConnect())
    {
        Console.WriteLine("✅ Connected successfully!\n");

        var customers = context.Customers.ToList();

        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.CustomerId}");
            Console.WriteLine($"Name: {customer.CustomerName}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine("----------------------");
        }

        Console.WriteLine($"\nTotal Customers: {customers.Count}");
    }
    else
    {
        Console.WriteLine("❌ Cannot connect to database.");
    }
}
catch (Exception ex)
{
    Console.WriteLine("❌ Error:");
    Console.WriteLine(ex.Message);
}

Console.ReadLine();

public class CustomerManagementContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=(localdb)\\MSSQLLocalDB;Database=CustomerManagementDB;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customer");
    }
}

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
}
