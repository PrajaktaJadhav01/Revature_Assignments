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

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid Input");
                    continue;
                }

                try
                {
                    // INSERT CUSTOMER
                    if (choice == 1)
                    {
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine() ?? "";

                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine() ?? "";

                        var customer = new Customer
                        {
                            CustomerName = name,
                            Email = email,
                            Phone = "9999999999",
                            Website = "www.company.com",
                            Industry = "IT",
                            CompanySize = "Medium",
                            Classification = "Active",
                            Type = "Individual",
                            SegmentId = null,
                            ParentCustomerId = null,
                            AccountValue = 60000,
                            HealthScore = 90,
                            CreatedDate = DateTime.Now,
                            ModifiedDate = DateTime.Now,
                            IsDeleted = false
                        };

                        db.Customers.Add(customer);
                        db.SaveChanges();

                        Console.WriteLine("Customer Inserted Successfully");
                    }

                    // INSERT ORDER
                    else if (choice == 2)
                    {
                        Console.Write("Enter CustomerId: ");
                        if (!int.TryParse(Console.ReadLine(), out int cid))
                        {
                            Console.WriteLine("Invalid CustomerId");
                            continue;
                        }

                        Console.Write("Enter Product Name: ");
                        string product = Console.ReadLine() ?? "";

                        Console.Write("Enter Quantity: ");
                        if (!int.TryParse(Console.ReadLine(), out int qty))
                        {
                            Console.WriteLine("Invalid Quantity");
                            continue;
                        }

                        Console.Write("Enter Total Amount: ");
                        if (!decimal.TryParse(Console.ReadLine(), out decimal amount))
                        {
                            Console.WriteLine("Invalid Amount");
                            continue;
                        }

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
                    }

                    // VIEW CUSTOMERS
                    else if (choice == 3)
                    {
                        var customers = db.Customers.ToList();

                        foreach (var c in customers)
                        {
                            Console.WriteLine($"{c.CustomerId} | {c.CustomerName} | {c.Email}");
                        }
                    }

                    // UPDATE CUSTOMER
                    else if (choice == 4)
                    {
                        Console.Write("Enter Customer Id: ");

                        if (!int.TryParse(Console.ReadLine(), out int id))
                        {
                            Console.WriteLine("Invalid Id");
                            continue;
                        }

                        var cust = db.Customers.Find(id);

                        if (cust != null)
                        {
                            Console.Write("Enter New Name: ");
                            cust.CustomerName = Console.ReadLine() ?? "";

                            Console.Write("Enter New Email: ");
                            cust.Email = Console.ReadLine() ?? "";

                            cust.ModifiedDate = DateTime.Now;

                            db.SaveChanges();

                            Console.WriteLine("Customer Updated Successfully");
                        }
                        else
                        {
                            Console.WriteLine("Customer Not Found");
                        }
                    }

                    // FILTER
                    else if (choice == 5)
                    {
                        var filtered = db.Customers
                            .Where(c => c.AccountValue > 50000)
                            .ToList();

                        foreach (var c in filtered)
                        {
                            Console.WriteLine($"{c.CustomerName} - {c.AccountValue}");
                        }
                    }

                    // SORT
                    else if (choice == 6)
                    {
                        var sorted = db.Customers
                            .OrderBy(c => c.CustomerName)
                            .ToList();

                        foreach (var c in sorted)
                        {
                            Console.WriteLine(c.CustomerName);
                        }
                    }

                    // JOIN
                    else if (choice == 7)
                    {
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
                    }

                    // GROUPING
                    else if (choice == 8)
                    {
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
                    }

                    // EAGER LOADING
                    else if (choice == 9)
                    {
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
                    }

                    // EXPLICIT LOADING
                    else if (choice == 10)
                    {
                        var explicitCustomer = db.Customers.FirstOrDefault();

                        if (explicitCustomer != null)
                        {
                            db.Entry(explicitCustomer)
                                .Collection(c => c.Orders)
                                .Load();

                            Console.WriteLine($"Customer: {explicitCustomer.CustomerName}");

                            foreach (var o in explicitCustomer.Orders)
                            {
                                Console.WriteLine(o.ProductName);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Customer Found");
                        }
                    }

                    // LAZY LOADING
                    else if (choice == 11)
                    {
                        var lazy = db.Customers.FirstOrDefault();

                        if (lazy != null)
                        {
                            Console.WriteLine($"Customer: {lazy.CustomerName}");

                            foreach (var o in lazy.Orders)
                            {
                                Console.WriteLine(o.ProductName);
                            }
                        }
                        else
                        {
                            Console.WriteLine("No Customer Found");
                        }
                    }

                    // PROJECTION
                    else if (choice == 12)
                    {
                        var projection = db.Customers
                            .Select(c => new
                            {
                                c.CustomerName
                            }).ToList();

                        foreach (var p in projection)
                        {
                            Console.WriteLine(p.CustomerName);
                        }
                    }

                    else if (choice == 13)
                    {
                        return;
                    }

                    else
                    {
                        Console.WriteLine("Invalid Choice");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);

                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Database Error: " + ex.InnerException.Message);
                    }
                }
            }
        }
    }
}