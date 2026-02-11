using System;
using System.Collections.Generic;
using System.Linq;

namespace Day6LinqAssignment
{
    
    class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
    }

    
    class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal OrderAmount { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            
            List<Customer> customers = new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName = "Prajakta" },
                new Customer { CustomerId = 2, CustomerName = "Tanaya" },
                new Customer { CustomerId = 3, CustomerName = "Harshali" }
            };

            
            List<Order> orders = new List<Order>
            {
                new Order { OrderId = 101, CustomerId = 1, OrderAmount = 500 },
                new Order { OrderId = 102, CustomerId = 1, OrderAmount = 300 },
                new Order { OrderId = 103, CustomerId = 2, OrderAmount = 700 },
                new Order { OrderId = 104, CustomerId = 2, OrderAmount = 200 },
                new Order { OrderId = 105, CustomerId = 2, OrderAmount = 100 }
            };

            
            var result = customers
                .Join(
                    orders,
                    c => c.CustomerId,
                    o => o.CustomerId,
                    (c, o) => new { c.CustomerName, o.OrderAmount }
                )
                .GroupBy(x => x.CustomerName)
                .Select(g => new
                {
                    CustomerName = g.Key,
                    OrderCount = g.Count(),
                    TotalOrderValue = g.Sum(x => x.OrderAmount)
                });

           
            Console.WriteLine("Customer Order Summary\n");

            foreach (var item in result)
            {
                Console.WriteLine("Customer Name : " + item.CustomerName);
                Console.WriteLine("Total Orders  : " + item.OrderCount);
                Console.WriteLine("Total Amount  : " + item.TotalOrderValue);
                Console.WriteLine("---------------------------");
            }

            Console.ReadLine();
        }
    }
}