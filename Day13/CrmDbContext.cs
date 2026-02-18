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
                Console.WriteLine("Active Customers:\n");

                var customers = context.Customers
                                       .Where(c => c.IsDeleted == false)
                                       .ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine(
                        $"Id: {customer.CustomerId}, " +
                        $"Name: {customer.CustomerName}, " +
                        $"Email: {customer.Email}, " +
                        $"Phone: {customer.Phone}");
                }
            }

            Console.ReadLine();
        }
    }

    // -------------------- DbContext --------------------

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
            // Map to correct table name
            modelBuilder.Entity<Customer>().ToTable("Customer");

            // Set primary key
            modelBuilder.Entity<Customer>().HasKey(c => c.CustomerId);
        }
    }

    // -------------------- Entity --------------------

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
