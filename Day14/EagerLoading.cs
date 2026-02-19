using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Proxies;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new CrmDbContext())
        {
            // ----------------------------
            // LAZY LOADING
            // ----------------------------
            Console.WriteLine("LAZY LOADING");

            var customer = context.Customers.FirstOrDefault();

            if (customer != null)
            {
                Console.WriteLine("Customer: " + customer.CustomerName);

                // Segment loads automatically
                Console.WriteLine("Segment: " + customer.Segment?.SegmentName);
            }

            // ----------------------------
            // EXPLICIT LOADING
            // ----------------------------
            Console.WriteLine("\nEXPLICIT LOADING");

            var customer2 = context.Customers.FirstOrDefault();

            if (customer2 != null)
            {
                context.Entry(customer2)
                       .Reference(c => c.Segment)
                       .Load();

                Console.WriteLine("Customer: " + customer2.CustomerName);
                Console.WriteLine("Segment: " + customer2.Segment?.SegmentName);
            }

            // ----------------------------
            // PROJECTION
            // ----------------------------
            Console.WriteLine("\nPROJECTION");

            var projectedData = context.Customers
                                       .Select(c => new
                                       {
                                           c.CustomerName,
                                           c.Email,
                                           c.AccountValue
                                       })
                                       .ToList();

            foreach (var item in projectedData)
            {
                Console.WriteLine(item.CustomerName + " - " + item.AccountValue);
            }

            // ----------------------------
            // ANONYMOUS TYPE WITH JOIN
            // ----------------------------
            Console.WriteLine("\nANONYMOUS TYPE WITH JOIN");

            var joinData = context.Customers
                                  .Join(context.Segments,
                                        c => c.SegmentId,
                                        s => s.SegmentId,
                                        (c, s) => new
                                        {
                                            c.CustomerName,
                                            s.SegmentName,
                                            c.AccountValue
                                        })
                                  .ToList();

            foreach (var item in joinData)
            {
                Console.WriteLine(item.CustomerName + " - " +
                                  item.SegmentName + " - " +
                                  item.AccountValue);
            }
        }
    }
}

public class CrmDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Segment> Segments { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer("Server=.\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True");
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

    public string? CustomerName { get; set; }

    public string? Email { get; set; }

    public decimal AccountValue { get; set; }

    public int? SegmentId { get; set; }

    public virtual Segment? Segment { get; set; }
}

public class Segment
{
    [Key]
    public int SegmentId { get; set; }

    public string? SegmentName { get; set; }

    public virtual ICollection<Customer>? Customers { get; set; }
}
