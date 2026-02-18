using System;
using System.Linq;

namespace CustomerManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new AppDbContext())
            {
                // INSERT
                var customer = new Customer
                {
                    Name = "EF Customer",
                    Age = 26
                };

                context.Customers.Add(customer);
                context.SaveChanges();
                Console.WriteLine("Customer Inserted using EF Core\n");

                // UPDATE
                var existingCustomer = context.Customers.FirstOrDefault(c => c.Id == 1);

                if (existingCustomer != null)
                {
                    existingCustomer.Name = "Updated via EF";
                    existingCustomer.Age = 35;
                    context.SaveChanges();
                    Console.WriteLine("Customer Updated using EF Core\n");
                }

                // READ
                var customers = context.Customers.ToList();

                Console.WriteLine("Customer List:");
                foreach (var c in customers)
                {
                    Console.WriteLine($"{c.Id} - {c.Name} - {c.Age}");
                }

                // DELETE (Optional)
                // var deleteCustomer = context.Customers.Find(2);
                // if (deleteCustomer != null)
                // {
                //     context.Customers.Remove(deleteCustomer);
                //     context.SaveChanges();
                //     Console.WriteLine("Customer Deleted using EF Core");
                // }
            }

            Console.ReadLine();
        }
    }
}