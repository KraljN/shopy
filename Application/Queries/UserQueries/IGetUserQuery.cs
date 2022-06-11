using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.UserQueries
{
    public interface IGetUserQuery : IQuery<int, UserDto>
    {
    }
}
