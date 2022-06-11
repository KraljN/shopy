using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.ProductQueries
{
    public interface IGetProductQuery : IQuery<int, ProductDto>
    {
    }
}
