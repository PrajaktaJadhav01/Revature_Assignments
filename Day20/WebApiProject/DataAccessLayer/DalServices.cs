using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using WebApiProject.Models;
using WebApiProject.DTOs;

namespace WebApiProject.DataAccessLayer
{
    public static class DalServices
    {
        public static void AddDataAccessLayer(this IServiceCollection services)
        {
            services.AddScoped<ICustomerService, CustomerService>();

           services.AddScoped<IValidator<CustomerDTO>, CustomerDTOValidator>();
        }
    }
}