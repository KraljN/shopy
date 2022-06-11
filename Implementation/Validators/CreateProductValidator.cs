using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateProductValidator : AbstractValidator<ProductDto>
    {
        public CreateProductValidator(Context context)
        {
            RuleFor(p => p.Name).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(p => p.Name)
                    .Must(name => !context.Products.Any(p => p.Name == name))
                    .WithMessage(dto => $"Product with name {dto.Name} already exists in database.");
                });
            RuleFor(p => p.Stock).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(p => p.Stock)
                    .GreaterThan(0)
                    .WithMessage("Product {PropertyName} must be greater than 0.");
                });

            RuleFor(p => p.Price).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.").
                DependentRules(() =>
                {
                    RuleFor(p => p.Price)
                    .GreaterThan(0)
                    .WithMessage("Product {PropertyName} must be greater than 0.");
                });

            RuleFor(p => p.Description).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.");

            RuleFor(p => p.Image).NotEmpty()
                .WithMessage("Product {PropertyName} must not be empty.");

            RuleFor(p => p.CategoryId).NotEmpty()
                .WithMessage("You must enter a category").
                DependentRules(() =>
                {
                    RuleFor(p => p.CategoryId)
                    .Must(id => context.Categories.Any(c => c.Id == id))
                    .WithMessage((dto, id) => $"Category with an id of {id} does not exists in database.");
                });
        }
    }
}
