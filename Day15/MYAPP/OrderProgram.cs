using System;
using System;

namespace MYAPP
{
    public class Order
    {
        public string Email { get; set; }
    }

    public class OrderRepository
    {
        public void Save(Order order)
        {
            Console.WriteLine($"Order saved for {order.Email}");
        }
    }

    public class EmailSender
    {
        public void Send(string email, string message)
        {
            Console.WriteLine($"Email sent to {email}: {message}");
        }
    }

    public class OrderProgram
    {
        public static void Run()
        {
            var repo = new OrderRepository();
            var email = new EmailSender();

            var order = new Order { Email = "john.doe@orderscompany.com" };

            repo.Save(order);
            email.Send(order.Email, "Order placed successfully!");
        }
    }
}