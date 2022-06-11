using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.ProductQueries;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.ProductQueries
{
    public class EFGetProductQuery : IGetProductQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetProductQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 14;

        public string Name => "Get single product with Entity Framework";

        public ProductDto Execute(int search)
        {
            var product = _context.Products
                .Include(x => x.Prices)
                .Include(x => x.Category)
                .FirstOrDefault(x => x.Id == search);

            if (product == null)
                throw new EntityNotFoundException(search, typeof(Product));

            return _mapper.Map<ProductDto>(product);
        }
    }
}
