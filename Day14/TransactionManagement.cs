using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

class Program
{
    static async Task Main(string[] args)
    {
        using (var context = new CrmDbContext())
        {
            // --------------------------------------------
            // TRANSACTION MANAGEMENT
            // --------------------------------------------
            Console.WriteLine("TRANSACTION MANAGEMENT");

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    var customer1 = await context.Customers
                        .FirstOrDefaultAsync(c => c.CustomerName == "Revature");

                    var customer2 = await context.Customers
                        .FirstOrDefaultAsync(c => c.CustomerName == "Tanaya Solutions");

                    if (customer1 != null && customer2 != null)
                    {
                        customer1.AccountValue += 10000;
                        customer2.AccountValue -= 5000;

                        await context.SaveChangesAsync();
                        await transaction.CommitAsync();

                        Console.WriteLine("Transaction committed successfully.");
                    }
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    Console.WriteLine("Transaction rolled back.");
                    Console.WriteLine(ex.Message);
                }
            }

            // --------------------------------------------
            // RAW SQL QUERY (SELECT)
            // --------------------------------------------
            Console.WriteLine("\nRAW SQL QUERY");

            var activeCustomers = context.Customers
                .FromSqlRaw("SELECT * FROM Customer WHERE Classification = 'Active'")
                .ToList();

            foreach (var customer in activeCustomers)
            {
                Console.WriteLine(customer.CustomerName + " - " + customer.AccountValue);
            }

            // --------------------------------------------
            // RAW SQL EXECUTE (UPDATE)
            // --------------------------------------------
            Console.WriteLine("\nRAW SQL UPDATE");

            int rowsAffected = context.Database.ExecuteSqlRaw(
                "UPDATE Customer SET AccountValue = AccountValue + 1000 WHERE Classification = 'Active'");

            Console.WriteLine("Rows Updated: " + rowsAffected);

            // --------------------------------------------
            // QUERY OPTIMIZATION
            // --------------------------------------------
            Console.WriteLine("\nQUERY OPTIMIZATION");

            var optimizedQuery = context.Customers
                .AsNoTracking()                  // No tracking for better performance
                .Where(c => c.AccountValue > 50000)
                .Select(c => new                 // Projection (only required columns)
                {
                    c.CustomerName,
                    c.AccountValue
                })
                .OrderByDescending(c => c.AccountValue)
                .ToList();

            foreach (var item in optimizedQuery)
            {
                Console.WriteLine(item.CustomerName + " - " + item.AccountValue);
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

    public string? CustomerName { get; set; }

    public string? Email { get; set; }

    public string? Classification { get; set; }

    public decimal AccountValue { get; set; }

    public int? SegmentId { get; set; }
}

public class Segment
{
    [Key]
    public int SegmentId { get; set; }

    public string? SegmentName { get; set; }
}
