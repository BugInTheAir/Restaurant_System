using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Application.Commands;

namespace User.Api.Application.Validations
{
    public class ChangePasswordWithRecoveryTokenCommandValidator  : AbstractValidator<ChangePasswordWithRecoveryTokenCommand>
    {
        public ChangePasswordWithRecoveryTokenCommandValidator()
        {
            RuleFor(u => u.NewPassword).NotEmpty().Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$").WithMessage("Invalid password");
            RuleFor(u => u.Token).NotEmpty().WithMessage("Invalid token");
        }
    }
}
