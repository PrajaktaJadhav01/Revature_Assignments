using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace CustomerManagementApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrmDbContext())
            {
                var customers = context.Customers
                                       .Include(c => c.Segment)
                                       .Where(c => c.IsDeleted == false)
                                       .ToList();

                Console.WriteLine("Active Customers:\n");

                foreach (var customer in customers)
                {
                    Console.WriteLine(
                        $"Id: {customer.CustomerId}, " +
                        $"Name: {customer.CustomerName}, " +
                        $"Email: {customer.Email}, " +
                        $"Phone: {customer.Phone}, " +
                        $"Segment: {customer.Segment?.SegmentName}");
                }
            }

            Console.ReadLine();
        }
    }

    // -------------------- DbContext --------------------

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

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Segment)
                .WithMany(s => s.Customers)
                .HasForeignKey(c => c.SegmentId);
        }
    }

    // -------------------- Customer --------------------

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

    // -------------------- Segment --------------------

    class Segment
    {
        public int SegmentId { get; set; }
        public string SegmentName { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<Customer>? Customers { get; set; }
    }
}
