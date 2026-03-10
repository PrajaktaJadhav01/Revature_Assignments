using System;
using System.Collections.Generic;
using System.Linq;

var uow = new UnitOfWork();

uow.Customers.Add(new Customer { Id = 1, Name = "Prajakta" });
uow.Orders.Add(new Order { Id = 1, ProductName = "Laptop", Quantity = 2 });

uow.Commit();

Console.WriteLine("Data saved successfully!");

class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
}

class Order
{
    public int Id { get; set; }
    public string ProductName { get; set; } = "";
    public int Quantity { get; set; }
}

class DatabaseContext
{
    public List<Customer> Customers { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}

class CustomerRepository
{
    private readonly DatabaseContext _context;

    public CustomerRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void Add(Customer customer) => _context.Customers.Add(customer);

    public IEnumerable<Customer> GetAll() => _context.Customers;

    public Customer? GetById(int id) =>
        _context.Customers.FirstOrDefault(c => c.Id == id);

    public void Remove(int id)
    {
        var customer = GetById(id);
        if (customer != null) _context.Customers.Remove(customer);
    }
}

class OrderRepository
{
    private readonly DatabaseContext _context;

    public OrderRepository(DatabaseContext context)
    {
        _context = context;
    }

    public void Add(Order order) => _context.Orders.Add(order);

    public IEnumerable<Order> GetAll() => _context.Orders;

    public Order? GetById(int id) =>
        _context.Orders.FirstOrDefault(o => o.Id == id);

    public void Remove(int id)
    {
        var order = GetById(id);
        if (order != null) _context.Orders.Remove(order);
    }
}

class UnitOfWork
{
    private readonly DatabaseContext _context = new();

    public CustomerRepository Customers { get; }
    public OrderRepository Orders { get; }

    public UnitOfWork()
    {
        Customers = new CustomerRepository(_context);
        Orders = new OrderRepository(_context);
    }

    public void Commit()
    {
        Console.WriteLine("Saving changes to database...");
    }
}