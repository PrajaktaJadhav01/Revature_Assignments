using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

class Program
{
    static void Main()
    {
        using var context = new CrmContext();
        var customers = context.Customers.ToList();

        foreach (var c in customers)
            Console.WriteLine($"{c.CustomerId}: {c.CustomerName} - {c.Email}");
    }
}

class CrmContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=localhost\\SQLEXPRESS;Database=CustomerManagementDB;Integrated Security=True;TrustServerCertificate=True"
        );
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customer");
    }
}

class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public bool IsDeleted { get; set; }
}