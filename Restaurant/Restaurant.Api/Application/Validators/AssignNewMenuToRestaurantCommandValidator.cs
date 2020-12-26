using FluentValidation;
using Restaurant.Api.Application.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurant.Api.Application.Validators
{
    public class AssignNewMenuToRestaurantCommandValidator : AbstractValidator<AssignNewMenuToRestaurantCommand>
    {
        public AssignNewMenuToRestaurantCommandValidator()
        {
            RuleFor(x => x.MenuId).NotEmpty().WithMessage("Invalid menu id");
            RuleFor(x => x.ResId).NotEmpty().WithMessage("Invalid res id");
        }
    }
}
