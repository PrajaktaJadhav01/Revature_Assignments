using AutoMapper;
using WebApiProject.Models;
using WebApiProject.DTOs;

namespace WebApiProject.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>();
            CreateMap<CustomerDTO, Customer>();
        }
    }
}