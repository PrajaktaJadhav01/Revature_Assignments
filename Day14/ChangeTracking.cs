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
            // ----------------------------------
            // CHANGE TRACKING + SaveChanges()
            // ----------------------------------

            Console.WriteLine("CHANGE TRACKING EXAMPLE");

            var customer = context.Customers
                                  .FirstOrDefault(c => c.CustomerName == "Tanaya Solutions");

            if (customer != null)
            {
                Console.WriteLine("Before Update: " + customer.AccountValue);

                customer.AccountValue += 10000;

                context.SaveChanges();

                Console.WriteLine("After Update: " + customer.AccountValue);
            }

            // ----------------------------------
            // AsNoTracking (Read Only Query)
            // ----------------------------------

            Console.WriteLine("\nAS NO TRACKING EXAMPLE");

            var readOnlyCustomer = context.Customers
                                          .AsNoTracking()
                                          .FirstOrDefault(c => c.CustomerName == "Revature");

            if (readOnlyCustomer != null)
            {
                Console.WriteLine("Fetched (Read Only): " + readOnlyCustomer.CustomerName);

                readOnlyCustomer.AccountValue += 5000;

                // This will NOT update database because entity is not tracked
                context.SaveChanges();

                Console.WriteLine("Value changed in memory but not saved to DB.");
            }

            // ----------------------------------
            // SaveChangesAsync()
            // ----------------------------------

            Console.WriteLine("\nSAVECHANGESASYNC EXAMPLE");

            var customerAsync = await context.Customers
                                             .FirstOrDefaultAsync(c => c.CustomerName == "Prajakta Consulting");

            if (customerAsync != null)
            {
                Console.WriteLine("Before Async Update: " + customerAsync.AccountValue);

                customerAsync.AccountValue += 20000;

                await context.SaveChangesAsync();

                Console.WriteLine("After Async Update: " + customerAsync.AccountValue);
            }

            // ----------------------------------
            // Check Entity State
            // ----------------------------------

            Console.WriteLine("\nENTITY STATE EXAMPLE");

            var entity = context.Customers.FirstOrDefault();

            if (entity != null)
            {
                Console.WriteLine("State Before Change: " + context.Entry(entity).State);

                entity.AccountValue += 1000;

                Console.WriteLine("State After Change: " + context.Entry(entity).State);
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

    public decimal AccountValue { get; set; }

    public int? SegmentId { get; set; }
}

public class Segment
{
    [Key]
    public int SegmentId { get; set; }

    public string? SegmentName { get; set; }
}
