using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrmDbContext())
            {
                Console.WriteLine("=== Active Customers ===\n");

                var customers = context.Customers
                    .Where(c => c.IsDeleted == false)
                    .ToList();

                foreach (var c in customers)
                {
                    Console.WriteLine($"Id: {c.CustomerId}");
                    Console.WriteLine($"Name: {c.CustomerName}");
                    Console.WriteLine($"Email: {c.Email}");
                    Console.WriteLine($"Phone: {c.Phone}");
                    Console.WriteLine("--------------------");
                }
            }

            Console.ReadLine();
        }
    }

    // ================== DB CONTEXT ==================
    class CrmDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;" +
                "Database=CustomerManagementDB;" +
                "Trusted_Connection=True;" +
                "TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }

    // ================== ENTITY ==================
    class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? SegmentId { get; set; }
        public bool IsDeleted { get; set; }
    }
}