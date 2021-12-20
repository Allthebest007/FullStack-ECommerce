using AutoMapper;
using InveonECommerce.Business.Engines.DTOs.AddressInfo;
using InveonECommerce.Business.Engines.DTOs.Category;
using InveonECommerce.Business.Engines.DTOs.Order;
using InveonECommerce.Business.Engines.DTOs.Product;
using InveonECommerce.Business.Engines.DTOs.User;
using InveonECommerce.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InveonECommerce.Business.Engines.Infrastructure
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ProductEntity, AddProductDTO>().ReverseMap();
            CreateMap<ProductImageEntity, AddProductDTO.ProductImageDTO>().ReverseMap();

            CreateMap<ProductEntity, UpdateProductDTO>().ReverseMap();
            CreateMap<ProductImageEntity, UpdateProductDTO.ProductImageDTO>().ReverseMap();

            CreateMap<ProductEntity, ProductDTO>().ReverseMap();
            CreateMap<ProductImageEntity, ProductDTO.ProductImageDTO>().ReverseMap();

            CreateMap<CategoryEntity, CategoryDTO>().ReverseMap();
            CreateMap<CategoryEntity, AddCategoryDTO>().ReverseMap();
            CreateMap<CategoryEntity, UpdateCategoryDTO>().ReverseMap();

            CreateMap<AddressInfoEntity, AddressInfoDTO>().ReverseMap();

            CreateMap<OrderDetailDTO, OrderEntity>().ReverseMap();
            CreateMap<ListOrderDTO, OrderEntity>().ReverseMap();

            CreateMap<UserEntity, UserDTO>().ReverseMap();
            

        }
    }
}
