using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.OrderQueries;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.OrderQueries
{
    public class EFGetOrderQuery : IGetOrderQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetOrderQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 18;

        public string Name => "Get single order with Entity Framework";

        public OrderDto Execute(int search)
        {
            var order = _context.Orders.Include(x => x.User).Include(x => x.OrderItems).FirstOrDefault(x => x.Id == search);

            if (order == null)
                throw new EntityNotFoundException(search, typeof(Order));

            return _mapper.Map<OrderDto>(order);
        }
    }
}
