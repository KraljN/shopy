using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries.CategoryQueries;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.CategoryQueries
{
    public class EFGetCategoryQuery : IGetCategoryQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetCategoryQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 9;

        public string Name => "Get single category with Entity Framework";

        public CategoryDto Execute(int search)
        {
            var category = _context.Categories.FirstOrDefault(x => x.Id == search);

            if (category == null)
            {
                throw new EntityNotFoundException(search, typeof(Category));
            }

            return _mapper.Map<CategoryDto>(category);
        }
    }
}
