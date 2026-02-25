using Microsoft.EntityFrameworkCore;

namespace CustomerManagement
{
    public class AppDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(
"Server=localhost\\SQLEXPRESS;Database=CustomerManagement;Trusted_Connection=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().ToTable("Customer");
        }
    }
}