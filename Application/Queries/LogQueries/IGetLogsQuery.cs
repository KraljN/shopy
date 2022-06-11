using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.LogQueries
{
    public interface IGetLogsQuery : IQuery<LogSearchDto, PagedResponse<LogDto>>
    {
    }
}
