using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.OrderCommands
{
    public interface IUpdateOrderCommand : ICommand<ChangeOrderStatusDto>
    {
    }
}
