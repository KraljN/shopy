using Application.DataTransfer;
using DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Validators
{
    public class UserLoginRequestValidator : AbstractValidator<UserLoginRequest>
    {
        public UserLoginRequestValidator(Context context)
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("You have to write email")
                .Must(x => context.Users.Any(u => u.Email == x)).WithMessage("User not found");

            RuleFor(x => x.Password).NotEmpty().WithMessage("You have to write password");
        }
    }
}
