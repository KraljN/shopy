using Application.DataTransfer;
using Application.Queries;
using Application.Queries.OrderQueries;
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

namespace Implementation.Queries.OrderQueries
{
    public class EFGetOrdersQuery : IGetOrdersQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetOrdersQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 17;

        public string Name => "Get multiple orders with Entity Framework";

        public PagedResponse<OrderDto> Execute(OrderSearchDto search)
        {
            var query = _context.Orders.Include(x => x.User).Include(x => x.OrderItems).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.OrderItems.Any(ol => ol.Product.Name.ToLower().Contains(search.Keyword.ToLower())) ||
                                            x.Address.ToLower().Contains(search.Keyword.ToLower()) ||
                                            x.User.FirstName.ToLower().Contains(search.Keyword.ToLower()) ||
                                            x.User.LastName.ToLower().Contains(search.Keyword.ToLower()));

            if (search.OrderDateMin.HasValue)
                query = query.Where(x => x.OrderDate >= search.OrderDateMin);

            if (search.OrderDateMax.HasValue)
                query = query.Where(x => x.OrderDate <= search.OrderDateMax);

            if (search.OrderItemQuantityMin.HasValue)
                query = query.Where(x => x.OrderItems.Any(oi => oi.Quantity >= search.OrderItemQuantityMin));

            if (search.OrderItemQuantityMax.HasValue)
                query = query.Where(x => x.OrderItems.Any(oi => oi.Quantity <= search.OrderItemQuantityMax));

            if (search.OrderItemsMin.HasValue)
                query = query.Where(x => x.OrderItems.Count() >= search.OrderItemsMin);

            if (search.OrderItemsMax.HasValue)
                query = query.Where(x => x.OrderItems.Count() <= search.OrderItemsMax);

            if (search.OrderStatus.HasValue)
                query = query.Where(x => x.OrderStatus == search.OrderStatus);

            if (search.PaymentMethod.HasValue)
                query = query.Where(x => x.PaymentMethod == search.PaymentMethod);

            if (search.UserIds.Count() > 0)
                query = query.Where(x => search.UserIds.Contains(x.UserId));

            if (search.ProductIds.Count() > 0)
                query = query.Where(x => x.OrderItems.Any(oi => search.ProductIds.Contains((int)oi.ProductId)));

            if (search.TotalPriceMin.HasValue)
                query = query.Where(x => x.OrderItems.Sum(oi => oi.Quantity * oi.Price) >= search.TotalPriceMin);

            if (search.TotalPriceMax.HasValue)
                query = query.Where(x => x.OrderItems.Sum(oi => oi.Quantity * oi.Price) <= search.TotalPriceMax);

            return query.Paged<OrderDto, Order>(search, _mapper);
        }
    }
}
