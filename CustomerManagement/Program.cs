using System;
using System.Linq;

namespace CustomerManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            using var db = new AppDbContext();

            while (true)
            {
                Console.WriteLine("\nCustomer Management System");
                Console.WriteLine("1. View Customers");
                Console.WriteLine("2. Add Customer");
                Console.WriteLine("3. Update Customer");
                Console.WriteLine("4. Delete Customer");
                Console.WriteLine("5. Exit");

                Console.Write("Enter choice: ");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        var customers = db.Customers.ToList();
                        foreach (var c in customers)
                        {
                            Console.WriteLine($"{c.CustomerId} | {c.CustomerName} | {c.Email} | {c.Phone}");
                        }
                        break;

                    case 2:
                        Console.Write("Enter Name: ");
                        string name = Console.ReadLine();

                        Console.Write("Enter Email: ");
                        string email = Console.ReadLine();

                        Console.Write("Enter Phone: ");
                        string phone = Console.ReadLine();

                        var newCustomer = new Customer
                        {
                            CustomerName = name,
                            Email = email,
                            Phone = phone
                        };

                        db.Customers.Add(newCustomer);
                        db.SaveChanges();

                        Console.WriteLine("Customer Added Successfully!");
                        break;

                    case 3:
                        Console.Write("Enter Customer ID to update: ");
                        int updateId = int.Parse(Console.ReadLine());

                        var cust = db.Customers.Find(updateId);

                        if (cust != null)
                        {
                            Console.Write("Enter New Phone: ");
                            cust.Phone = Console.ReadLine();

                            db.SaveChanges();
                            Console.WriteLine("Customer Updated!");
                        }
                        else
                        {
                            Console.WriteLine("Customer Not Found.");
                        }
                        break;

                    case 4:
                        Console.Write("Enter Customer ID to delete: ");
                        int deleteId = int.Parse(Console.ReadLine());

                        var customer = db.Customers.Find(deleteId);

                        if (customer != null)
                        {
                            db.Customers.Remove(customer);
                            db.SaveChanges();
                            Console.WriteLine("Customer Deleted!");
                        }
                        else
                        {
                            Console.WriteLine("Customer Not Found.");
                        }
                        break;

                    case 5:
                        return;
                }
            }
        }
    }
}
