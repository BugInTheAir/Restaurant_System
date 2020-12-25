using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Application.Commands;

namespace User.Api.Application.Validations
{
    public class UpdateUserPasswordCommandValidator : AbstractValidator<UpdateUserPasswordCommand>
    {
        public UpdateUserPasswordCommandValidator()
        {
            RuleFor(u => u.UserName).NotEmpty().WithMessage("No user name found");
            RuleFor(u => u.DTO.NewPassword).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").WithMessage("Invalid password");
            RuleFor(u => u.DTO.ConfirmPassword).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").WithMessage("Invalid confirm password");
        }
    }
}
