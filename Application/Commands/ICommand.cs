using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands
{
    public interface ICommand<TRequest> : IUseCase
    {
        void Execute(TRequest request);
    }
}
