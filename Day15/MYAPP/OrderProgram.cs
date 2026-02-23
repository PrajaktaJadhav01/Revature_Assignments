namespace MYAPP
{
    public interface IOrderRepository
    {
        void Save(Order order);
    }

    public interface IEmailSender
    {
        void Send(string email, string message);
    }

    public class Order
    {
        public string Email { get; set; } = string.Empty;
    }

    public class OrderRepository : IOrderRepository
    {
        public void Save(Order order)
        {
        }
    }

    public class EmailSender : IEmailSender
    {
        public void Send(string email, string message)
        {
        }
    }

    public class OrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IEmailSender _emailSender;

        public OrderService(IOrderRepository repository, IEmailSender emailSender)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }

        public void PlaceOrder(Order order)
        {
            if (order == null)
                throw new ArgumentNullException(nameof(order));

            _repository.Save(order);
            _emailSender.Send(order.Email, "Order placed successfully!");
        }
    }
}