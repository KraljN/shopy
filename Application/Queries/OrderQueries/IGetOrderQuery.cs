using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.OrderQueries
{
    public interface IGetOrderQuery : IQuery<int, OrderDto>
    {
    }
}
