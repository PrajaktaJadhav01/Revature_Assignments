using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                using (var context = new CrmDbContext())
                {
                    var customers = context.Customers
                                           .Where(c => c.IsDeleted == false)
                                           .ToList();

                    Console.WriteLine("Active Customers:\n");

                    foreach (var customer in customers)
                    {
                        Console.WriteLine($"Customer ID: {customer.CustomerId}");
                        Console.WriteLine($"Name: {customer.CustomerName}");
                        Console.WriteLine($"Email: {customer.Email}");
                        Console.WriteLine($"Phone: {customer.Phone}");
                        Console.WriteLine("----------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            Console.ReadLine();
        }
    }

    public class CrmDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // VERY IMPORTANT FIX
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }

    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? SegmentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}