using System.Collections.Generic;

namespace WebApiProject.Models
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();

        Customer? GetCustomerById(int id);

        void AddCustomer(Customer customer);

        void UpdateCustomer(int id, Customer customer);

        void DeleteCustomer(int id);
    }
}