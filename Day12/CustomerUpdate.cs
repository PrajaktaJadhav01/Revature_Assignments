using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CrmContext())
            {
                // Find customer by ID
                var customer = context.Customers
                                      .FirstOrDefault(c => c.CustomerId == 2);

                if (customer != null)
                {
                    // Update values
                    customer.Phone = "9111111111";
                    customer.Email = "updated@revature.com";

                    // Save changes to database
                    context.SaveChanges();

                    Console.WriteLine("Customer Updated Successfully!");
                }
                else
                {
                    Console.WriteLine("Customer not found!");
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
