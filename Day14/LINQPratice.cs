using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new CrmDbContext())
        {
            Console.WriteLine("===== ALL CUSTOMERS =====");

            var customers = context.Customers.ToList();

            foreach (var customer in customers)
            {
                Console.WriteLine($"ID: {customer.CustomerId}, Name: {customer.CustomerName}, Email: {customer.Email}");
            }

            Console.WriteLine("\n===== ACTIVE CUSTOMERS =====");

            var activeCustomers = context.Customers
                                         .Where(c => c.Classification == "Active")
                                         .ToList();

            foreach (var customer in activeCustomers)
            {
                Console.WriteLine(customer.CustomerName);
            }

            Console.WriteLine("\n===== SELECT Name and AccountValue =====");

            var selectedData = context.Customers
                                      .Select(c => new
                                      {
                                          c.CustomerName,
                                          c.AccountValue
                                      })
                                      .ToList();

            foreach (var item in selectedData)
            {
                Console.WriteLine($"{item.CustomerName} - {item.AccountValue}");
            }

            Console.WriteLine("\n===== ORDER BY AccountValue DESC =====");

            var orderedCustomers = context.Customers
                                          .OrderByDescending(c => c.AccountValue)
                                          .ToList();

            foreach (var customer in orderedCustomers)
            {
                Console.WriteLine($"{customer.CustomerName} - {customer.AccountValue}");
            }

            Console.WriteLine("\n===== JOIN Customer and Segment =====");

            var joinedData = context.Customers
                .Join(context.Segments,
                      c => c.SegmentId,
                      s => s.SegmentId,
                      (c, s) => new
                      {
                          c.CustomerName,
                          s.SegmentName
                      })
                .ToList();

            foreach (var item in joinedData)
            {
                Console.WriteLine($"{item.CustomerName} - {item.SegmentName}");
            }
        }
    }
}

public class CrmDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Segment> Segments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(
            "Server=.\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().ToTable("Customer");
        modelBuilder.Entity<Segment>().ToTable("Segment");
    }
}

public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    public string CustomerName { get; set; }

    public string Email { get; set; }

    public string Classification { get; set; }

    public decimal AccountValue { get; set; }

    public int? SegmentId { get; set; }

    public Segment Segment { get; set; }
}

public class Segment
{
    [Key]
    public int SegmentId { get; set; }

    public string SegmentName { get; set; }

    public ICollection<Customer> Customers { get; set; }
}
