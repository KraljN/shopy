﻿using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ProductCommands
{
    public interface IUpdateProductCommand : ICommand<ProductDto>
    {
    }
}
