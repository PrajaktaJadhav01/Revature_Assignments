using System;

namespace CustomerManagement
{
public class Order
{
public int OrderId { get; set; }


    public int CustomerId { get; set; }

    public string? ProductName { get; set; }

    public int Quantity { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Customer? Customer { get; set; }
}


}
