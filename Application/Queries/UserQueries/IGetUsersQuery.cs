
using Application.DataTransfer;
using Application.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.UserQueries
{
    public interface IGetUsersQuery : IQuery<UserSearchDto, PagedResponse<UserDto>>
    {
    }
}
