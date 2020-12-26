using FluentValidation;
using Restaurant.Api.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Validators
{
    public class ChangeRestaurantAddressCommandValidator : AbstractValidator<ChangeRestaurantAddressCommand>
    {
        public ChangeRestaurantAddressCommandValidator()
        {
            RuleFor(x => x.District).NotEmpty().WithMessage("Invalid district");
            RuleFor(x => x.Street).NotEmpty().WithMessage("Invalid street");
            RuleFor(x => x.Ward).NotEmpty().WithMessage("Invalid ward");
            RuleFor(x => x.ResId).NotEmpty().WithMessage("Invalid res id");
        }
    }
}
