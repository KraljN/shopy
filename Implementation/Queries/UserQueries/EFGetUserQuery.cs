using Application.DataTransfer;
using Application.Exceptions;
using Application.Queries;
using Application.Queries.UserQueries;
using AutoMapper;
using DataAccess;
using Domen.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Queries.UserQueries
{
    public class EFGetUserQuery : IGetUserQuery
    {
        private readonly Context _context;
        private readonly IMapper _mapper;

        public EFGetUserQuery(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 21;

        public string Name => "Get single user with Entity Framework";

        public UserDto Execute(int search)
        {
            var user = _context.Users.Find(search);

            if (user == null)
                throw new EntityNotFoundException(search, typeof(User));

            return _mapper.Map<UserDto>(user);
        }
    }
}
