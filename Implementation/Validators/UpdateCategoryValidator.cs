using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public  class UpdateCategoryValidator : AbstractValidator<CategoryDto>
    {
        public UpdateCategoryValidator(Context context)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("You have to choose Category Name");

            RuleFor(x => x.Name).Must((dto, name) => !context.Categories.Any(y => y.Name == name && y.Id != dto.Id))
                 .WithMessage(dto => $"Category {dto.Name} already exists.");
        }
    }
}
