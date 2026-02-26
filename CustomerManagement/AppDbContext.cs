using Microsoft.EntityFrameworkCore;

namespace CustomerManagement
{
public class AppDbContext : DbContext
{
public DbSet<Customer> Customers { get; set; } = null!;
public DbSet<Order> Orders { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies()
            .UseSqlServer("Server=.\\SQLEXPRESS;Database=CustomerManagementDB;Trusted_Connection=True;TrustServerCertificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map entity to correct table name
        modelBuilder.Entity<Customer>().ToTable("Customer");

        // Relationship: Customer -> Orders
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
    }
}

}
