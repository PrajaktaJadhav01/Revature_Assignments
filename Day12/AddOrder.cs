using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrmContext())
            {
                // ---------------- ADD ORDER ----------------
                var newOrder = new Order
                {
                    Product = "Laptop",
                    Price = 55000,
                    CustomerId = 1   // Must exist in Customer table
                };

                context.Orders.Add(newOrder);
                context.SaveChanges();

                Console.WriteLine("Order Added Successfully!\n");

                // ---------------- SHOW ORDERS ----------------
                var orders = context.Orders
                                    .Include(o => o.Customer)
                                    .ToList();

                foreach (var order in orders)
                {
                    Console.WriteLine($"OrderId: {order.OrderId}");
                    Console.WriteLine($"Product: {order.Product}");
                    Console.WriteLine($"Price: {order.Price}");
                    Console.WriteLine($"Customer: {order.Customer.CustomerName}");
                    Console.WriteLine("-----------------------------------");
                }
            }

            Console.ReadLine();
        }
    }

    // -------------------- DbContext --------------------

    class CrmContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=localhost\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
            modelBuilder.Entity<Order>().ToTable("Order");
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

        public ICollection<Order>? Orders { get; set; }
    }

    // -------------------- Order --------------------

    class Order
    {
        public int OrderId { get; set; }
        public string Product { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
    }
}
