using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Application.Commands;

namespace User.Api.Application.Validations
{
    public class ValidateEmailCommandValidator : AbstractValidator<ValidateEmailCommand>
    {
        public ValidateEmailCommandValidator()
        {
            RuleFor(e => e.EmailToValidate).NotEmpty().Matches(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").WithMessage("Invalid email");
        }
    }
}
