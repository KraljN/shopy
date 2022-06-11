using Application.DataTransfer;
using Domen.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;


namespace Implementation.Profiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();
        }
    }
}
