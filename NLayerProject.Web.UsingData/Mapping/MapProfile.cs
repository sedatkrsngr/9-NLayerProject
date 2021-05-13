
using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using NLayerProject.Web.UsingData.Dtos;

namespace NLayerProject.Web.UsingData.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
           
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Category, CategoryGetProductsDto>();
            CreateMap<CategoryGetProductsDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();

            CreateMap<Product, ProductGetCategoryDto>();
            CreateMap<ProductGetCategoryDto, Product>();
        }
    }
}
