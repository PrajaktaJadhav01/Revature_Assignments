using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CustomerManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();

            while (true)
            {
                Console.WriteLine("\nMENU");
                Console.WriteLine("1. Insert Customer");
                Console.WriteLine("2. Insert Order");
                Console.WriteLine("3. View All Customers");
                Console.WriteLine("4. Update Customer");
                Console.WriteLine("5. Filter Customers (AccountValue > 50000)");
                Console.WriteLine("6. Sort Customers By Name");
                Console.WriteLine("7. Join Customers & Orders");
                Console.WriteLine("8. Grouping & Aggregation");
                Console.WriteLine("9. Eager Loading");
                Console.WriteLine("10. Explicit Loading");
                Console.WriteLine("11. Lazy Loading");
                Console.WriteLine("12. Projection");
                Console.WriteLine("13. Exit");

                Console.Write("\nEnter choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();

                        var customer = new Customer
                        {
                            CustomerName = name,
                            Email = email
                        };

                        db.Customers.Add(customer);
                        db.SaveChanges();

                        Console.WriteLine("Customer Inserted Successfully");
                        break;

                    case 2:
                        Console.Write("Enter CustomerId: ");
                        int cid = int.Parse(Console.ReadLine());

                        Console.Write("Enter Product Name: ");
                        string product = Console.ReadLine();

                        Console.Write("Enter Quantity: ");
                        int qty = int.Parse(Console.ReadLine());

                        Console.Write("Enter Total Amount: ");
                        decimal amount = decimal.Parse(Console.ReadLine());

                        var order = new Order
                        {
                            CustomerId = cid,
                            ProductName = product,
                            Quantity = qty,
                            TotalAmount = amount
                        };

                        db.Orders.Add(order);
                        db.SaveChanges();

                        Console.WriteLine("Order Inserted Successfully");
                        break;

                    case 3:
                        var customers = db.Customers.ToList();

                        foreach (var c in customers)
                        {
                            Console.WriteLine($"{c.CustomerId} | {c.CustomerName} | {c.Email}");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Customer Id to Update: ");
                        int updateId = int.Parse(Console.ReadLine());

                        var cust = db.Customers.Find(updateId);

                        if (cust != null)
                        {
                            Console.Write("Enter New Name: ");
                            cust.CustomerName = Console.ReadLine();

                            Console.Write("Enter New Email: ");
                            cust.Email = Console.ReadLine();

                            db.SaveChanges();

                            Console.WriteLine("Customer Updated Successfully");
                        }
                        else
                        {
                            Console.WriteLine("Customer Not Found");
                        }
                        break;

                    case 5:
                        var filtered = db.Customers
                            .Where(c => c.AccountValue > 50000)
                            .ToList();

                        foreach (var c in filtered)
                        {
                            Console.WriteLine($"{c.CustomerName} - {c.AccountValue}");
                        }
                        break;

                    case 6:
                        var sorted = db.Customers
                            .OrderBy(c => c.CustomerName)
                            .ToList();

                        foreach (var c in sorted)
                        {
                            Console.WriteLine($"{c.CustomerName}");
                        }
                        break;

                    case 7:
                        var joinData = db.Customers
                            .Join(db.Orders,
                            c => c.CustomerId,
                            o => o.CustomerId,
                            (c, o) => new
                            {
                                c.CustomerName,
                                o.ProductName,
                                o.TotalAmount
                            }).ToList();

                        foreach (var item in joinData)
                        {
                            Console.WriteLine($"{item.CustomerName} | {item.ProductName} | {item.TotalAmount}");
                        }
                        break;

                    case 8:
                        var group = db.Orders
                            .GroupBy(o => o.CustomerId)
                            .Select(g => new
                            {
                                CustomerId = g.Key,
                                TotalOrders = g.Count(),
                                TotalAmount = g.Sum(x => x.TotalAmount)
                            }).ToList();

                        foreach (var g in group)
                        {
                            Console.WriteLine($"Customer {g.CustomerId} Orders: {g.TotalOrders} Amount: {g.TotalAmount}");
                        }
                        break;

                    case 9:
                        var eager = db.Customers
                            .Include(c => c.Orders)
                            .ToList();

                        foreach (var c in eager)
                        {
                            Console.WriteLine($"Customer: {c.CustomerName}");

                            foreach (var o in c.Orders)
                            {
                                Console.WriteLine($"   Order: {o.ProductName}");
                            }
                        }
                        break;

                    case 10:
                        var explicitCustomer = db.Customers.FirstOrDefault();

                        db.Entry(explicitCustomer)
                          .Collection(c => c.Orders)
                          .Load();

                        Console.WriteLine($"Customer: {explicitCustomer.CustomerName}");

                        foreach (var o in explicitCustomer.Orders)
                        {
                            Console.WriteLine(o.ProductName);
                        }
                        break;

                    case 11:
                        var lazy = db.Customers.FirstOrDefault();

                        Console.WriteLine($"Customer: {lazy.CustomerName}");

                        foreach (var o in lazy.Orders)
                        {
                            Console.WriteLine(o.ProductName);
                        }
                        break;

                    case 12:
                        var projection = db.Customers
                            .Select(c => new
                            {
                                c.CustomerName
                            }).ToList();

                        foreach (var p in projection)
                        {
                            Console.WriteLine(p.CustomerName);
                        }
                        break;

                    case 13:
                        return;

                    default:
                        Console.WriteLine("Invalid choice");
                        break;
                }
            }
        }
    }
}