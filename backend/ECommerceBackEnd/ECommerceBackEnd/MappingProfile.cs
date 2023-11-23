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
            CreateMap<CustomerDTO, Customer>();
            CreateMap<CustomerDTO, Customer>().ReverseMap();
            CreateMap<OrderDto,Order>();
            CreateMap<OrderDto,Order>().ReverseMap();
            CreateMap<CreateOrderDto, Order>();
            CreateMap<UpdateOrderStatusDto, Order>();
            CreateMap<UpdateOrderLocationDto, Order>();
            CreateMap<Customer, Order>();
            CreateMap<Product, OrderDetail>().ForMember(c => c.OrderItemCardprodId, opt => opt.MapFrom(x => x.ProductCardId));
            CreateMap<Order, OrderDetail>();
            CreateMap<Category, OrderDetail>();
            CreateMap<OrderDetailDto, OrderDetail>();
            CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
            CreateMap<CreateOrderDetailDto, OrderDetail>();
            CreateMap<UpdateOrderDetailDto, OrderDetail>();
        }
    }
}
