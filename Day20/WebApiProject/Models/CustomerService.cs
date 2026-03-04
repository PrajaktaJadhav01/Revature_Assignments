using System.Collections.Generic;

namespace WebApiProject.Models
{
   public class CustomerService : ICustomerService
    {
        private static List<Customer> customers = new List<Customer>()
        {
            new Customer { Id = 1, Name = "Prajakta", Email = "prajakta@gmail.com" },
            new Customer { Id = 2, Name = "Rahul", Email = "rahul@gmail.com" }
        };

        public List<Customer> GetAllCustomers()
        {
            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            return customers.FirstOrDefault(c => c.Id == id);
        }

        public void AddCustomer(Customer customer)
        {
            customers.Add(customer);
        }

        public void UpdateCustomer(int id, Customer updatedCustomer)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customer.Name = updatedCustomer.Name;
                customer.Email = updatedCustomer.Email;
            }
        }

        public void DeleteCustomer(int id)
        {
            var customer = customers.FirstOrDefault(c => c.Id == id);
            if (customer != null)
            {
                customers.Remove(customer);
            }
        }
    }
}