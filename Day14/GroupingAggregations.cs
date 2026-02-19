using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new CrmDbContext())
        {
            // -----------------------------
            // MARK CUSTOMER AS INACTIVE (Close)
            // -----------------------------
            var customerToClose = context.Customers
                                         .FirstOrDefault(c => c.CustomerName == "Prajakta Consulting");

            if (customerToClose != null)
            {
                customerToClose.Classification = "Inactive";
                context.SaveChanges();
                Console.WriteLine("Customer marked as Inactive successfully.");
            }

            // -----------------------------
            // GROUPING: Group customers by Segment
            // -----------------------------
            Console.WriteLine("\nCustomers Grouped By Segment:");

            var groupedCustomers =
                context.Customers
                       .GroupBy(c => c.SegmentId)
                       .Select(g => new
                       {
                           SegmentId = g.Key,
                           TotalCustomers = g.Count()
                       })
                       .ToList();

            foreach (var group in groupedCustomers)
            {
                Console.WriteLine($"Segment: {group.SegmentId}, Total Customers: {group.TotalCustomers}");
            }

            // -----------------------------
            // AGGREGATIONS
            // -----------------------------
            Console.WriteLine("\nAggregations:");

            var totalCustomers = context.Customers.Count();
            var totalAccountValue = context.Customers.Sum(c => c.AccountValue);
            var averageAccountValue = context.Customers.Average(c => c.AccountValue);
            var maxAccountValue = context.Customers.Max(c => c.AccountValue);
            var minAccountValue = context.Customers.Min(c => c.AccountValue);

            Console.WriteLine($"Total Customers: {totalCustomers}");
            Console.WriteLine($"Total Account Value: {totalAccountValue}");
            Console.WriteLine($"Average Account Value: {averageAccountValue}");
            Console.WriteLine($"Maximum Account Value: {maxAccountValue}");
            Console.WriteLine($"Minimum Account Value: {minAccountValue}");

            // -----------------------------
            // GROUPING WITH JOIN (Customer + Segment)
            // -----------------------------
            Console.WriteLine("\nGrouping With Segment Name:");

            var groupedWithSegment =
                context.Customers
                       .Join(context.Segments,
                             c => c.SegmentId,
                             s => s.SegmentId,
                             (c, s) => new { c, s })
                       .GroupBy(cs => cs.s.SegmentName)
                       .Select(g => new
                       {
                           SegmentName = g.Key,
                           CustomerCount = g.Count()
                       })
                       .ToList();

            foreach (var item in groupedWithSegment)
            {
                Console.WriteLine($"{item.SegmentName} - {item.CustomerCount}");
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
}

public class Segment
{
    [Key]
    public int SegmentId { get; set; }

    public string SegmentName { get; set; }
}
