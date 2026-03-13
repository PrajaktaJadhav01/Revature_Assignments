using WebApiProject.Data;
using WebApiProject.Models;

namespace WebApiProject.Commands
{
    public class AddCustomerCommand : ICommand
    {
        private readonly AppDbContext _context;
        private readonly Customer _customer;

        public AddCustomerCommand(AppDbContext context, Customer customer)
        {
            _context = context;
            _customer = customer;
        }

        public void Execute()
        {
            _context.Customers.Add(_customer);
            _context.SaveChanges();
        }
    }
}