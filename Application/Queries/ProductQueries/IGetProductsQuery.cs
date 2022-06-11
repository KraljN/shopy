using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.ProductQueries
{
    public interface IGetProductsQuery : IQuery<ProductSearchDto, PagedResponse<ProductDto>>
    { 
    }
}
