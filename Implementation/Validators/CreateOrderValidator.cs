using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Validators
{
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        public CreateOrderValidator(Context context)
        {
            RuleFor(x => x.OrderDate)
                .GreaterThan(DateTime.Today)
                .WithMessage("Order date must be in future.");

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage("Address is required.");

            RuleFor(x => x.PaymentMethod)
                .NotNull()
                .WithMessage("Payment Method is required.");

            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("UserId is required.")
                .Must(id => context.Users.Any(user => user.Id == id))
                .WithMessage((dto, id) => $"User with an id of {id} does not exists in database.");

            RuleFor(x => x.OrderItems)
                .NotEmpty()
                .WithMessage("There must be at least one order item.")
                .Must(oi => oi.Select(x => x.ProductId).Distinct().Count() == oi.Count())
                .WithMessage("Duplicate products are not allowed.")
                .DependentRules(() =>
                {
                    RuleForEach(x => x.OrderItems).SetValidator(new CreateOrderItemValidator(context));
                });
        }
    }
}
