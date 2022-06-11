using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IQuery<TSearch, TResult> : IUseCase
    {
        TResult Execute(TSearch search);
    }
}
