using System;
using System.Collections.Generic;
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
                Console.WriteLine("===== ALL ACTIVE CUSTOMERS =====\n");

                var customers = context.Customers
                                       .Where(c => c.IsDeleted == false)
                                       .ToList();

                foreach (var customer in customers)
                {
                    Console.WriteLine($"Id: {customer.CustomerId}");
                    Console.WriteLine($"Name: {customer.CustomerName}");
                    Console.WriteLine($"Email: {customer.Email}");
                    Console.WriteLine($"Phone: {customer.Phone}");
                    Console.WriteLine("----------------------------");
                }

                Console.ReadLine();
            }
        }
    }

    // ================= DB CONTEXT =================
    class CrmDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Segment> Segments { get; set; }

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
            modelBuilder.Entity<Segment>().ToTable("Segment");
        }
    }

    // ================= CUSTOMER TABLE =================
    class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? SegmentId { get; set; }
        public bool IsDeleted { get; set; }

        public Segment? Segment { get; set; }
    }

    // ================= SEGMENT TABLE =================
    class Segment
    {
        public int SegmentId { get; set; }
        public string? SegmentName { get; set; }
        public string? Description { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}