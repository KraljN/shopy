using Application.DataTransfer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.CategoryCommands
{
    public interface ICreateCategoryCommand : ICommand<CategoryDto>
    {
    }
}
