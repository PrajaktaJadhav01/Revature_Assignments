using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrmContext())
            {
                Console.WriteLine("Adding 4 new customers...\n");

                // Add 4 Records
                context.Customers.Add(new Customer
                {
                    CustomerName = "Customer One",
                    Email = "one@test.com",
                    Phone = "9000000011",
                    SegmentId = 1,
                    IsDeleted = false
                });

                context.Customers.Add(new Customer
                {
                    CustomerName = "Customer Two",
                    Email = "two@test.com",
                    Phone = "9000000022",
                    SegmentId = 1,
                    IsDeleted = false
                });

                context.Customers.Add(new Customer
                {
                    CustomerName = "Customer Three",
                    Email = "three@test.com",
                    Phone = "9000000033",
                    SegmentId = 1,
                    IsDeleted = false
                });

                context.Customers.Add(new Customer
                {
                    CustomerName = "Customer Four",
                    Email = "four@test.com",
                    Phone = "9000000044",
                    SegmentId = 1,
                    IsDeleted = false
                });

                context.SaveChanges();

                Console.WriteLine("Records inserted successfully!\n");

                // Fetch Active Customers
                var customers = context.Customers
                                       .Where(c => c.IsDeleted == false)
                                       .ToList();

                Console.WriteLine("Active Customers:\n");

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
    class CrmContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
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
