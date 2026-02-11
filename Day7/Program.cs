using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        OrderProcessor processor = new OrderProcessor();

        processor.Process("SKU-100", 2);
        processor.Process("", -1);
        processor.Process("SKU-999", 1);
        processor.Process("SKU-200", 3);
    }
}

public class OrderProcessor
{
    private Dictionary<string, decimal> _catalog = new Dictionary<string, decimal>()
    {
        {"SKU-100", 29.99m},
        {"SKU-200", 49.99m},
        {"SKU-300", 99.99m}
    };

    private List<Order> _savedOrders = new List<Order>();

    public void Process(string sku, int quantity)
    {
        try
        {
            Order order = CreateOrder(sku, quantity);
            Save(order);
            Console.WriteLine("Saved order: " + order.Sku + ", qty " + order.Quantity + ", total " + order.Total);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to process order: " + ex.Message);
        }
    }

    private Order CreateOrder(string sku, int quantity)
    {
        Validate(sku, quantity);
        decimal price = LookupPrice(sku);
        return new Order(sku, quantity, price * quantity);
    }

    private void Validate(string sku, int quantity)
    {
        if (string.IsNullOrWhiteSpace(sku))
            throw new ArgumentException("SKU is required");

        if (quantity <= 0)
            throw new ArgumentException("Quantity must be positive");
    }

    private decimal LookupPrice(string sku)
    {
        if (!_catalog.ContainsKey(sku))
            throw new KeyNotFoundException("Unknown SKU: " + sku);

        return _catalog[sku];
    }

    private void Save(Order order)
    {
        try
        {
            Random rnd = new Random();

            if (rnd.Next(5) == 0)
                throw new IOException("Connection timeout");

            _savedOrders.Add(order);
        }
        catch (IOException)
        {
            Console.WriteLine("Save failed, continuing...");
        }
    }
}

public class Order
{
    public string Sku { get; set; }
    public int Quantity { get; set; }
    public decimal Total { get; set; }

    public Order(string sku, int quantity, decimal total)
    {
        Sku = sku;
        Quantity = quantity;
        Total = total;
    }
}
