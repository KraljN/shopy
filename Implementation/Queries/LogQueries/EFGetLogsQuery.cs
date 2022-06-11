using Application.DataTransfer;
using Application.Queries;
using Application.Queries.LogQueries;
using Application.Searches;
using AutoMapper;
using DataAccess;
using Domen;
using Domen.Entities;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.LogQueries
{
    public class EFGetLogsQuery : IGetLogsQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetLogsQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public int Id => 26;

        public string Name => "Get logs";

        public PagedResponse<LogDto> Execute(LogSearchDto search)
        {
            var logs = _context.UseCaseLogs.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                logs = logs.Where(x => x.Actor.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.UseCaseName.ToLower().Contains(search.Keyword.ToLower()));

            if (search.DateFrom.HasValue)
                logs = logs.Where(x => x.CreatedAt >= search.DateFrom);

            if (search.DateTo.HasValue)
                logs = logs.Where(x => x.CreatedAt <= search.DateTo);

            return logs.Paged<LogDto, UseCaseLog>(search, _mapper);
        }
    }
}
