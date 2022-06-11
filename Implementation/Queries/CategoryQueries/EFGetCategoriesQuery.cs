using Application.DataTransfer;
using Application.Queries;
using Application.Queries.CategoryQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using Implementation.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CategoryQueries
{
    public class EFGetCategoriesQuery : IGetCategoriesQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetCategoriesQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 8;

        public string Name => "Get categories with Entity Framework";

        public PagedResponse<CategoryDto> Execute(CategorySearchDto search)
        {
            var categories = _context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                categories = categories.Where(x => x.Name.Contains(search.Name));
            }
            return categories.Paged<CategoryDto, Category>(search, _mapper);
        }
    }
}
