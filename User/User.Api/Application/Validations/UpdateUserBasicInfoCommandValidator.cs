using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using User.Api.Application.Commands;

namespace User.Api.Application.Validations
{
    public class UpdateUserBasicInfoCommandValidator : AbstractValidator<UpdateUserBasicInfoCommand>
    {
        public UpdateUserBasicInfoCommandValidator()
        {
            RuleFor(u => u.Dto.District).NotEmpty().WithMessage("No district found");
            RuleFor(u => u.Dto.Street).NotEmpty().WithMessage("no Street found");
            RuleFor(u => u.UserName).NotEmpty().WithMessage("No user name found");
            RuleFor(u => u.Dto.Phone).NotEmpty().Matches(@"\(?\d{3}\)?-? *\d{3}-? *-?\d{4}").WithMessage("Invalid phone number");
            RuleFor(u => u.Dto.Name).NotEmpty().WithMessage("No name found");
            RuleFor(u => u.Dto.Ward).NotEmpty().WithMessage("No ward found");
        }
    }
}
