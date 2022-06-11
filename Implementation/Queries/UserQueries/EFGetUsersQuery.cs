
using Application.DataTransfer;
using Application.Queries;
using Application.Queries.UserQueries;
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

namespace Implementation.Queries.UserQueries
{
    public class EFGetUsersQuery : IGetUsersQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetUsersQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 20;

        public string Name => "Get users with Entity Framework";

        public PagedResponse<UserDto> Execute(UserSearchDto search)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword) && !string.IsNullOrWhiteSpace(search.Keyword))
                query = query.Where(x => x.FirstName.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.LastName.ToLower().Contains(search.Keyword.ToLower()) ||
                                        x.Email.ToLower().Contains(search.Keyword.ToLower()));

            return query.Paged<UserDto, User>(search, _mapper);
        }
    }
}
