using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class CreateUserValidator : AbstractValidator<UserDto>
    {
        public CreateUserValidator(Context context)
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("You have to write First Name")
                .MaximumLength(20).WithMessage("First Name has 30 characters maximum");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("You have to write Last Name")
                .MaximumLength(20).WithMessage("Last Name has 30 characters maximum");

            RuleFor(x => x.Email).NotEmpty().WithMessage("You have to write E-mail")
                .EmailAddress().WithMessage("Invalid E-mail format")
                .Must((dto, email) => !context.Users.Any(user => user.Email == email))
                .WithMessage(dto => $"E-mail address {dto.Email} already exists");

            RuleFor(x => x.Password).NotEmpty().WithMessage("You have to write Password")
                .MinimumLength(8).WithMessage("Password has 8 characters minimum");


            RuleFor(x => x.ConfirmPassword).NotEmpty().WithMessage("You have to write Password Confirm")
                .Equal(x => x.Password).WithMessage("Passwords do not match");
        }
    }
}
