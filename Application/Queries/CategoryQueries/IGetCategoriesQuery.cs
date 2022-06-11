using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.CategoryQueries
{
    public interface IGetCategoriesQuery : IQuery<CategorySearchDto, PagedResponse<CategoryDto>>
    {
    }
}
