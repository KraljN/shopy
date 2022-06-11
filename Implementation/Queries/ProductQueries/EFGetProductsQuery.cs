using Application.DataTransfer;
using Application.Queries;
using Application.Queries.ProductQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domen;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Implementation.Extensions;
using Domen.Entities;

namespace Implementation.Queries.ProductQueries
{
    public class EFGetProductsQuery : IGetProductsQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetProductsQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 13;

        public string Name => "Get products with Entity Framework";

        public PagedResponse<ProductDto> Execute(ProductSearchDto search)
        {
            var products = _context.Products
                .Include(x => x.Prices)
                .Include(x => x.Category)
                .AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                products = products.Where(x => x.Name.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.Description.ToLower().Contains(search.Keyword.ToLower()));

            if (search.CategoryIds.Count() > 0)
                products = products.Where(x => search.CategoryIds.Contains(x.CategoryId));

            if (search.MinPrice.HasValue)
                products = products.Where(x => x.Prices.OrderByDescending(p => p.CreatedAt).First().PriceAmount >= search.MinPrice);

            if (search.MaxPrice.HasValue)
                products = products.Where(x => x.Prices.OrderByDescending(p => p.CreatedAt).First().PriceAmount <= search.MinPrice);

            return products.Paged<ProductDto, Product>(search, _mapper);
        }
    }
}
