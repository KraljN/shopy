using Application.DataTransfer;
using AutoMapper;
using Domen.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Profiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dto => dto.CategoryName, opt => opt.MapFrom(product => product.Category.Name))
                .ForMember(dto => dto.Image, opt => opt.Ignore())
                .ForMember(dto => dto.Price, opt => opt.MapFrom(product => product.Prices.OrderByDescending(p => p.CreatedAt).Select(p => p.PriceAmount).First()));

            CreateMap<ProductDto, Product>()
                .ForMember(product => product.Image, opt => opt.Ignore());
        }
    }
}
