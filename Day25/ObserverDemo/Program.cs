using System;

var customerService = new CustomerService();

var admin = new User() { Name = "Admin" };
var manager = new User() { Name = "Manager" };

// Subscribe to event
customerService.CustomerAdded += admin.CustomerListener;
customerService.CustomerAdded += manager.CustomerListener;

// Add customer
customerService.AddCustomer(new Customer { Id = 1, Name = "Prajakta", Email = "prajakta@gmail.com" });

// Unsubscribe
customerService.CustomerAdded -= admin.CustomerListener;

// Add another customer
customerService.AddCustomer(new Customer { Id = 2, Name = "Sneha", Email = "sneha@gmail.com" });


// ---------------- USER ----------------

public class User
{
    public string Name { get; set; }

    public void CustomerListener(Customer customer)
    {
        Console.WriteLine($"{Name} Notification: New Customer Added -> {customer.Name} ({customer.Email})");
    }
}


// ---------------- CUSTOMER ----------------

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
}


// ---------------- SERVICE ----------------

public class CustomerService
{
    public event Action<Customer> CustomerAdded;

    public void AddCustomer(Customer customer)
    {
        Console.WriteLine($"Customer Added: {customer.Name}");

        NotifyCustomerAdded(customer);
    }

    void NotifyCustomerAdded(Customer customer)
    {
        CustomerAdded?.Invoke(customer);
    }
}