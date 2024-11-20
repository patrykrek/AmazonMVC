using AmazonMVC.DTO;
using AmazonMVC.Models;
using AutoMapper;

namespace AmazonMVC.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, GetCategoryDTO>();
            CreateMap<AddCategoryDTO, Category>();
            CreateMap<Product, GetProductDTO>();
            CreateMap<AddProductDTO, Product>();
            CreateMap<CartItem, GetCartDTO>();
            CreateMap<AddCartDTO, CartItem>();
            CreateMap<Order, GetOrderDTO>();
        }
    }
}
