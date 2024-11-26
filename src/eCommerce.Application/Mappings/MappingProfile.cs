using AutoMapper;
using eCommerce.Application.DTOs.Product;
using eCommerce.Domain.Entities;

namespace eCommerce.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, CreateProductDTO>().ReverseMap();
            CreateMap<Product, UpdateProductDTO>().ReverseMap();
            CreateMap<Product, ViewProductDTO>();
        }
    }
}
