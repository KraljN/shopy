
using Application;
using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UserDto>
    {
        public UpdateUserValidator(Context context, IApplicationActor actor)
        {
            RuleFor(x => x.FirstName)
            .NotEmpty()
            .WithMessage("First Name is required.")
            .DependentRules(() =>
            {
                RuleFor(x => x.FirstName)
                .MaximumLength(30)
                .WithMessage("First Name can have a maximum of 30 characters");
            });
            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("Last Name is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.LastName)
                    .MaximumLength(30)
                    .WithMessage("Last Name can have a maximum of 30 characters");
                });
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("User {PropertyName} is required.")
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email)
                    .EmailAddress()
                    .WithMessage("A valid email address is required.")
                    .DependentRules(() =>
                    {
                        RuleFor(x => x.Email)
                        .Must((dto, email) => !context.Users.Any(user => user.Email == email && user.Id != dto.Id))
                        .WithMessage(dto => $"The email address of {dto.Email} already exists in the database");
                    });
                });
            When(x => !string.IsNullOrWhiteSpace(x.Password), () =>
            {
                RuleFor(x => x.Password)
                    .MinimumLength(8);
                RuleFor(x => x.ConfirmPassword)
                    .NotEmpty()
                    .WithMessage("Confirm Password is required")
                    .Equal(x => x.Password)
                    .WithMessage("Password and Confirm Password do not match");
            });

        }
    }
}
