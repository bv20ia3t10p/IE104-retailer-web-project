using AutoMapper;
using ECommerceBackEnd.Dtos;
using ECommerceBackEnd.Entities;

namespace ECommerceBackEnd
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<Product, ProductDto>();
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<CreateProductDto,Product>();
            CreateMap<UpdateProductDto, Product>();
        }
    }
}
